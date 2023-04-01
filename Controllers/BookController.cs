using BookStore.API.Models;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook([FromBody] BookModel book)
        {
            var id = await _bookRepository.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBookById), new { id = id, controller = "book" }, await GetBookById(id));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookModel book)
        {
            await _bookRepository.UpdateBookAsync(id, book);
            return Ok();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateBookPatch([FromRoute] int id, [FromBody] JasonPatchDocument book)
        {
            await _bookRepository.UpdateBookPatchAsync(id, book);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return Ok();
        }
    }
}
