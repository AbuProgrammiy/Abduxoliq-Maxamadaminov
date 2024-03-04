using BookStore.Application.Abstractions.IRepositories;         // IOrderRepository ishlashi uchun
using BookStore.Application.Abstractions.IServices;             // IOrderService ishlashi uchun
using BookStore.Domain.Entities.DTOs;
using BookStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public string Create(OrderDTO bookDTO)
        {
            Order model = new Order()
            {
                BookName=bookDTO.BookName,
                Category=bookDTO.Category,
                OwnerName=bookDTO.OwnerName,
                PurchaseDate=DateTime.UtcNow,
            };

            return _orderRepository.Create(model);
        }

        public string Delete(int id)
        {
            return _orderRepository.Delete(x=> x.Id == id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order GetByName(string bookName)
        {
            return _orderRepository.GetByAny(x=>x.BookName == bookName);
        }

        public string Update(int id, OrderDTO bookDTO)
        {
            Order model = _orderRepository.GetByAny(x => x.Id == id);
            return _orderRepository.Update(model);
        }
    }
}
