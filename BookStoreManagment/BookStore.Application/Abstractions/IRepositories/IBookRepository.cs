using BookStore.Domain.Entities.Models;             // Book ishlashi uchun

namespace BookStore.Application.Abstractions.IRepositories
{
    public interface IBookRepository:IBaseRepository<Book>
    {
    }
}
