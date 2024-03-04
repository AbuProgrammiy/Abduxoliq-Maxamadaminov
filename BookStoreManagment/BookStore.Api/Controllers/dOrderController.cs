using BookStore.Api.Attributes;
using BookStore.Application.Abstractions.IServices;
using BookStore.Domain.Entities.DTOs;         // OrderDTO ishlashi uchun
using BookStore.Domain.Entities.Enums;
using BookStore.Domain.Entities.Models;       // Order ishlashi uchun
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;               // ControllerBase ishlashi uchun

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class dOrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public dOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [IdentityFilter(Permissions.Create)]
        public string Create(OrderDTO orderDTO)
        {
            return _orderService.Create(orderDTO);  
        }

        [HttpGet]
        [IdentityFilter(Permissions.Get)]
        public IEnumerable<Order> GetAll()
        {
            return _orderService.GetAll();
        }

        [HttpGet]
        [IdentityFilter(Permissions.Get)]
        public Order GetByName(string name)
        {
            return _orderService.GetByName(name);
        }

        [HttpPut]
        [IdentityFilter(Permissions.Update)]
        public string Update(int id,OrderDTO orderDTO)
        {
           return _orderService.Update(id, orderDTO);
        }

        [HttpDelete]
        [IdentityFilter(Permissions.Delete)]
        public string Delete(int id)
        {
            return _orderService.Delete(id);    
        }
    }
}
