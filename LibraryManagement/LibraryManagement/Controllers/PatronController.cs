using LibraryManagement.DTO;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class PatronController : ControllerBase
    {
        public readonly AppDbContext _context;
        public PatronController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Patron> patron = _context.Patrons.ToList();
            return Ok(patron);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Patron patron = _context.Patrons.FirstOrDefault(p => p.ID == id);  
            return Ok(patron);
        }
        [HttpPost]
        public IActionResult ADD (PatronDTO patrondto)
        {
            var patron = new Patron()
            {
                Name = patrondto.Name,
                ContactInformation = patrondto.ContactInformation,
            };
            _context.Patrons.Add(patron);
            _context.SaveChanges();
            return Ok(patron);
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdatePatron(int id , PatronDTO patrondto)
        {
            var oldpatron = _context.Patrons.FirstOrDefault(p => p.ID == id);
            if(oldpatron == null)
            {
                return NotFound();
            }
            oldpatron.Name = patrondto.Name;
            oldpatron.ContactInformation = patrondto.ContactInformation;
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeletePatron(int id)
        {
            Patron patron = _context.Patrons.FirstOrDefault(p => p.ID == id);
            if(patron != null)
            {
                _context.Patrons.Remove(patron);
                _context.SaveChanges();
                return StatusCode(204 , "Recored is deleted");
            }
            return BadRequest("Id Not Found");
        }
    }
}
