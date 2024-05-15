using LibraryManagement.DTO;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BookController : ControllerBase
    {
        public readonly AppDbContext _context;
        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Book> books = _context.Books.ToList();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Book book = _context.Books.FirstOrDefault(b => b.ID == id);
            return Ok(book);
        }

        //[HttpPost]
        //public IActionResult AddNew(Book book)
        //{
        //    if (ModelState.IsValid == true)
        //    {
        //        _context.Books.Add(book);
        //        _context.SaveChanges();
        //    }
        //    return BadRequest(ModelState);
        //}

        [HttpPost]
        public IActionResult ADD (BookDTO bookdto)
        {
            var book = new Book()
            {
                Title = bookdto.Title,
                Author = bookdto.Author,
                PublicationYear = bookdto.PublicationYear,
                ISBN = bookdto.ISBN,
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            return Ok(book);
        }

        //[HttpPut("{id:int}")]
        //public IActionResult UpdateBook(int id , Book UpdateBook)
        //{
        //    Book OldBook = _context.Books.FirstOrDefault(k => k.ID == id);
        //    if(OldBook != null)
        //    {
        //        return NotFound();
        //    }
        //    OldBook.Title = UpdateBook.Title;
        //    OldBook.Author = UpdateBook.Author;
        //    OldBook.PublicationYear = UpdateBook.PublicationYear;
        //    OldBook.ISBN = UpdateBook.ISBN;

        //    return Ok(OldBook);
        //}
        
        [HttpPut("{id:int}")]
        public IActionResult UpdateBook(int id, BookDTO bookdto)
        {
            var OldBook = _context.Books.FirstOrDefault(b => b.ID == id);
            if (OldBook == null)
            {
                return NotFound();
            }
            OldBook.Title = bookdto.Title;
            OldBook.Author = bookdto.Author;
            OldBook.PublicationYear = bookdto.PublicationYear;
            OldBook.ISBN = bookdto.ISBN;
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook (int id)
        {
            Book DeleteBook = _context.Books.FirstOrDefault(s => s.ID == id);
            if(DeleteBook != null)
            {
                _context.Books.Remove(DeleteBook);
                _context.SaveChanges();
                return StatusCode(204, "Record is deleted");
            }
            return BadRequest("Id Not Found");
        }
    }
}
