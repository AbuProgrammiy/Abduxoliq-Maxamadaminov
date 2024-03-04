using BookStore.Domain.Entities.DTOs;           // OrderDTO ishlashi uchun
using BookStore.Domain.Entities.Models;         // Order ishlashi uchun

namespace BookStore.Application.Abstractions.IServices
{
    public interface IOrderService
    {
        public string Create(OrderDTO bookDTO);
        public IEnumerable<Order> GetAll();
        public Order GetByName(string name);
        public string Update(int id, OrderDTO bookDTO);
        public string Delete(int id);
    }
}
