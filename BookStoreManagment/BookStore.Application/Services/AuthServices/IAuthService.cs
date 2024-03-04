using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entities.DTOs; //User model ishlashi uchun


namespace BookStore.Application.Services.AuthServices
{
    public interface IAuthService
    {
        public string GenerateToken(UserDTO user);

    }
}
