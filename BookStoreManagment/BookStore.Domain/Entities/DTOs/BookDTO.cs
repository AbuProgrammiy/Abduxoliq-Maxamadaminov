using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookStore.Domain.Entities.DTOs
{
    public class BookDTO
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public double Rating { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public IFormFile Image { get; set; }
    }
}
