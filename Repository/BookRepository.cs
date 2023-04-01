using BookStore.API.Controllers;
using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var record = await _context.Books.Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description= x.Description,
            }).ToListAsync();

            return record;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            var result = await _context.Books.Where(x => x.Id == id).Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
            }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<int> AddBookAsync(BookModel bookModel)
        {
            var book = new Books()
            {
                Title = bookModel.Title,
                Description = bookModel.Description,
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task UpdateBookAsync(int id, BookModel book)
        {
            //var findBook = await _context.Books.FindAsync(id);
            //if(findBook != null)
            //{
            //    findBook.Title = book.Title;
            //    findBook.Description = book.Description;

            //    await _context.SaveChangesAsync();
            //}

            var newBook = new Books()
            {
                Id = id,
                Title = book.Title,
                Description = book.Description
            };

            _context.Books.Update(newBook);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateBookPatchAsync(int id, JasonPatchDocument book)
        {
            var findBook = await _context.Books.FindAsync(id);
            if (findBook != null)
            {
                book.ApplyTo(findBook);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = new Books()
            {
                Id = id,
            };

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
