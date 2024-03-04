using BookStore.Domain.Entities.DTOs;
using BookStore.Domain.Entities.Models;           // UserDTO

namespace BookStore.Application.Abstractions.IRepositories
{
    public interface IUserRepository:IBaseRepository<User>
    {
    }
}
