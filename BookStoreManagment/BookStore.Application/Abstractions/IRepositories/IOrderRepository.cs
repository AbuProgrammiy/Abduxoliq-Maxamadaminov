using BookStore.Domain.Entities.Models;     // Order ishlashi uchun

namespace BookStore.Application.Abstractions.IRepositories
{
    public interface IOrderRepository:IBaseRepository<Order>
    {
    }
}
