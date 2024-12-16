using ApiLoginToken.Dto;
using ApiLoginToken.Models;

namespace ApiLoginToken.Interfaces
{
    public interface IUserRepository
    {

        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> CreateAsync(User usersModel);

        Task<User?> UpdateAsync(int id, UpdateUsersDto usersDto);

        Task<User?> DeleteAsync(int id);
    }
}
