using LibraryManagement.DTO;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;    
        public AccountController(UserManager<ApplicationUser> userManager , IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Registration (RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser AppUser = new ApplicationUser();
                AppUser.UserName = registerDTO.UserName;
                AppUser.Email = registerDTO.Password;
                IdentityResult result = await userManager.CreateAsync(AppUser, registerDTO.Password);
                if (result.Succeeded)
                {
                    return Ok("Account Created");
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }
        public async Task<IActionResult> Login (LoginUserDTO loginDTO)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = await userManager.FindByNameAsync(loginDTO.UserName);
                if(user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user ,loginDTO.Password);
                    if (found)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name,user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var itemRole in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, itemRole));
                        }
                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));
                        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer : config["JWT:ValidIssuer"],
                            audience : config["JWT:ValidAudience"],
                            claims:claims,
                            expires:DateTime.Now.AddHours(1),
                            signingCredentials:signingCredentials
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(myToken),
                            expiration = myToken.ValidTo
                        });
                    }
                    return Unauthorized();
                }
            }
            return Unauthorized();
        }
    }
}
