using BookStore.Application.Abstractions.IRepositories;
using BookStore.Application.Abstractions.IServices;
using BookStore.Domain.Entities.DTOs;
using BookStore.Domain.Entities.Models;
using System.Linq.Expressions;         // IUserRepositoryishlashi uchun

namespace BookStore.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public string Create(UserDTO userDTO)
        {
            User model = new User()
            {
                UserName = userDTO.UserName,
                Password=userDTO.Password,
                Email = userDTO.Email,
                Role = userDTO.Role,
            };

            return _userRepository.Create(model);
        }

        public string Delete(int id)
        {
            return _userRepository.Delete(x => x.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetByName(string userName)
        {
            return _userRepository.GetByAny(x => x.UserName == userName);
        }

        public string Update(int id, UserDTO bookDTO)
        {
            User model = _userRepository.GetByAny(x => x.Id == id);
            return _userRepository.Update(model);
        }
    }
}
