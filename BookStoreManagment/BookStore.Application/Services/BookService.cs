using BookStore.Application.Abstractions.IRepositories;     // IBookRepository ishlashi uchun
using BookStore.Application.Abstractions.IServices;         // IBookService ishlashi uchun
using BookStore.Domain.Entities.DTOs;
using BookStore.Domain.Entities.Models;                     // Book ishlashi uchun

namespace BookStore.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookService)
        {
            _bookRepository = bookService;
        }

        public string Create(BookDTO bookDTO)
        {
            Book book = new Book()
            {
                BookName = bookDTO.BookName,
                Author = bookDTO.Author,
                Category = bookDTO.Category,
                Rating = bookDTO.Rating,
                Quantity = bookDTO.Quantity,
                Price = bookDTO.Price,
                ImagePath = bookDTO.Image.FileName
            };

            return _bookRepository.Create(book);

        }

        public string Delete(int id)
        {
            return _bookRepository.Delete(x=>x.Id==id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }

        public Book GetByName(string bookName)
        {
            return _bookRepository.GetByAny(x => x.BookName == bookName);
        }

        public string Update(int id, BookDTO bookDTO)
        {
            Book model = _bookRepository.GetByAny(x => x.Id == id);
            model.BookName = bookDTO.BookName;
            model.Author = bookDTO.Author;
            model.Category = bookDTO.Category;
            model.Rating = bookDTO.Rating;
            model.Quantity = bookDTO.Quantity;
            model.Price = bookDTO.Price;
            model.ImagePath= bookDTO.Image.FileName;

            return _bookRepository.Update(model);
        }
    }
}
