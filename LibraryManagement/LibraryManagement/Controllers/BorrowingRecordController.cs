using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Models
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class BorrowingRecordController : ControllerBase
    {
        public readonly AppDbContext _context;
        public BorrowingRecordController(AppDbContext context)
        {
            _context = context; 
        }
        [HttpPost]
        public IActionResult AddBorrowingRecord(BorrowingRecord record)
        {
            if (ModelState.IsValid == true)
            {
                _context.BorrowingRecords.Add(record);
                _context.SaveChanges();
            }
            return BadRequest(ModelState);
        }
        ////[HttpPut("{id:int}")]
        ////public IActionResult UpdateRecord(int id, BorrowingRecord record)
        ////{
        ////    var OldRecord = _context.BorrowingRecords.FirstOrDefault(b => b.ID == id);
        ////    if (OldRecord == null)
        ////    {
        ////        return NotFound();
        ////    }
        ////}
    }
}
