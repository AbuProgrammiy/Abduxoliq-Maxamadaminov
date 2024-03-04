using BookStore.Domain.Entities.DTOs;           // UserDTO ishlashi uchun
using BookStore.Domain.Entities.Models;         // User ishlashi uchun

namespace BookStore.Application.Abstractions.IServices
{
    public interface IUserService
    {
        public string Create(UserDTO userDTO);
        public IEnumerable<User> GetAll();
        public User GetByName(string name);
        public string Update(int id, UserDTO userDTO);
        public string Delete(int id);
    }
}
