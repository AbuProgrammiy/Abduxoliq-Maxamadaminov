using BookStore.Domain.Entities.DTOs;           // BookDTO ishlashi uchun
using BookStore.Domain.Entities.Models;         // Book ishlashi uchun
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Abstractions.IServices
{
    public interface IBookService
    {
        public string Create(BookDTO bookDTO);
        public IEnumerable<Book> GetAll();
        public Book GetByName(string name);
        public string Update(int id, BookDTO bookDTO);
        public string Delete(int id);
    }
}
