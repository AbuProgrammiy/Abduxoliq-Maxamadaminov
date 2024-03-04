using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.DTOs
{
    public class OrderDTO
    {
        public string BookName { get; set; }
        public string Category { get; set; }
        public string OwnerName { get; set; }
    }
}
