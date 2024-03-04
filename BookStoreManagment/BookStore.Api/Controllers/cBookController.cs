using BookStore.Api.Attributes;
using BookStore.Application.Abstractions.IServices;
using BookStore.Domain.Entities.DTOs;       // BookDTO ishlashi uchun
using BookStore.Domain.Entities.Enums;
using BookStore.Domain.Entities.Models;     // Book ishlashi uchun
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;             // ControllerBase ishlashi uchun


// Ushbu Controllerda logic yozilganiga sabab ushbu controllerda fayllar bilan ishlangan
// va IWebHostEnvironment tipi faqat Controller ichida taniydi!
namespace BookStore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class cBookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IWebHostEnvironment _webHost;

        public cBookController(IBookService bookService, IWebHostEnvironment webHost)
        {
            _bookService = bookService;
            _webHost = webHost;
        }

        [HttpPost]
        [IdentityFilter(Permissions.Create)]
        public string Create(BookDTO bookDTO)
        {
            string path = Path.Combine(_webHost.WebRootPath, "images", bookDTO.Image.FileName);
            using(FileStream stream=new FileStream(path,FileMode.Create))
            {
                bookDTO.Image.CopyToAsync(stream).GetAwaiter().GetResult();
            }
            return _bookService.Create(bookDTO);
        }

        [HttpGet]
        [IdentityFilter(Permissions.Get)]
        public IEnumerable<Book> GetAll()
        {
            return _bookService.GetAll();
        }

        [HttpGet]
        [IdentityFilter(Permissions.Get)]
        public Book GetByName(string Name)
        {
            return _bookService.GetByName(Name);
        }

        [HttpPut]
        [IdentityFilter(Permissions.Update)]
        public string Update(int id,BookDTO bookDTO)
        {
            return _bookService.Update(id, bookDTO);
        }

        [HttpDelete]
        [IdentityFilter(Permissions.Delete)]
        public string Delete(int id)
        {
            return _bookService.Delete(id);
        }
    }
}
