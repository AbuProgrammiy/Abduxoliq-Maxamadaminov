using BookStore.Application.Abstractions.IRepositories;         // IOrderRepository ishlashi uchun
using BookStore.Domain.Entities.Models;                         // Order ishlashi uchun
using BookStore.Infrastructure.BaseRepositories;                // BaseRepository ishlashi uchun
using BookStore.Infrastructure.Persistance;                     // BookStoreDbContext ishlashi uchun

namespace BookStore.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(BookStoreDbContext context) : base(context)
        {
            
        }
    }
}
