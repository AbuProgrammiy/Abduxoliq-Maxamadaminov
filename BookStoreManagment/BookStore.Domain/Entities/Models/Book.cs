using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public double Rating { get; set; }
        public int Quantity { get; set; }
        public double Price {  get; set; }
        public string ImagePath {  get; set; }
    }
}