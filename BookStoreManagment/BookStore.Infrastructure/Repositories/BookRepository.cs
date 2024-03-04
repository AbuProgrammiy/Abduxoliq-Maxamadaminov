using BookStore.Application.Abstractions.IRepositories;     // IBookRepository ishlatish uchun
using BookStore.Domain.Entities.Models;                     // Book ishlashi uchun
using BookStore.Infrastructure.BaseRepositories;            // BaseRepository ishlashi uchun
using BookStore.Infrastructure.Persistance;                 // BookStoreDbContextishlashi uchun

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository:BaseRepository<Book>,IBookRepository
    {
        public BookRepository(BookStoreDbContext context):base(context)
        {
            
        }
    }
}
