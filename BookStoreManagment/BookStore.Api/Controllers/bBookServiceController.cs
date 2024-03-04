using BookStore.Api.Attributes;
using BookStore.Application.Abstractions.IServices;
using BookStore.Domain.Entities.DTOs;
using BookStore.Domain.Entities.Enums;
using BookStore.Domain.Entities.Models;         // Order ishlashi uchun
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;                 // ControllerBase ishlashi uchun

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class bBookServiceController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IOrderService _orderService;

        public bBookServiceController(IBookService bookService,IOrderService orderService)
        {
            _bookService = bookService;
            _orderService = orderService;
        }

        [HttpGet]
        [IdentityFilter(Permissions.Observe)]
        public IEnumerable<Book> GetObserve()
        {
            return _bookService.GetAll();
        }

        [HttpPut]
        [IdentityFilter(Permissions.BookBuy)]
        public string BuyBook(string bookName, string yourName)
        {
            

            Book book=_bookService.GetByName(bookName);
            OrderDTO order = new OrderDTO()
            {
                BookName = bookName,
                Category = book.Category,
                OwnerName = yourName,
            };
            _orderService.Create(order);

            return "xarid amalga oshdi!";
        }

        [HttpGet]
        [IdentityFilter(Permissions.BookOrder)]
        public IEnumerable<Order> MyOrders(string ownerName)
        {
            IEnumerable<Order> orders= _orderService.GetAll();
            return orders.Where(x => x.OwnerName == ownerName);
        }

        [HttpPatch]
        [IdentityFilter(Permissions.BookRate)]
        public string RateBook(string bookName, double rate)
        {
            if (rate<1||rate > 9)
                return "Baho 1-9 oraligida bo'lishi kerak";

            Book book = _bookService.GetByName(bookName);
            BookDTO bookDTO = new BookDTO()
            {
                BookName = book.BookName,
                Author = book.Author,
                Category = book.Category,
                Rating = (book.Rating + rate) / 2,
                Quantity = book.Quantity,
                Price = book.Price,
            };
            _bookService.Update(book.Id,bookDTO);

            return "Baho berganingiz uchun tashakkur";
        }

    }
}
