using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Category { get; set; }
        public string OwnerName { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}