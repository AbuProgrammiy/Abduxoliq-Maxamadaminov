using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities.DTOs
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
