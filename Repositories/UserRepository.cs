using Microsoft.EntityFrameworkCore;
using ApiLoginToken.Data;
using ApiLoginToken.Dto;
using ApiLoginToken.Models;
using ApiLoginToken.Interfaces;

namespace ApiLoginToken.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;

        }


        public async Task<User> CreateAsync(User usersModel)
        {
            await _context.Users.AddAsync(usersModel);
            await _context.SaveChangesAsync();
            return usersModel;
        }

        public async Task<User?> DeleteAsync(int id)
        {
            var usersModel = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (usersModel == null)
            {
                return null;
            }

            _context.Users.Remove(usersModel);
            await _context.SaveChangesAsync();
            return usersModel;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> UpdateAsync(int id, UpdateUsersDto usersDto)
        {
            var existingUsers = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (existingUsers == null)
            {
                return null;
            }

            existingUsers.Nome = usersDto.Nome;
            existingUsers.Senha = usersDto.Senha;
            await _context.SaveChangesAsync();
            return existingUsers;
        }
    }

    }


