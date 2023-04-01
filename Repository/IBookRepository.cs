using BookStore.API.Controllers;
using BookStore.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookById(int id);
        Task<int> AddBookAsync(BookModel bookModel);
        Task UpdateBookAsync(int id, BookModel book);
        Task UpdateBookPatchAsync(int id, JasonPatchDocument book);
        Task DeleteBookAsync(int id);
    }
}
