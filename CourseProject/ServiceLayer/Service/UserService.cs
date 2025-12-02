using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.DTO_s;
using ServiceLayer.Models;

namespace ServiceLayer.Service
{
    public class UserService
    {
        private readonly ForestryContext _context;
        public UserService(ForestryContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAllResposibleAsync()
        {
            try
            {
                var users = await _context.Users
                    .Include(u => u.Role)
                    .Where(u => u.RoleId != 4)
                    .ToListAsync();

                if (users == null)
                    return null;

                return users.Select(user => new UserDto
                {
                    FullName = user.LastName + " " + user.Name + " " + user.Patronymic,
                    RoleName = user.Role.Name,
                }).ToList();
            }
            catch (DbException)
            {
                throw;
            }
        }
        public async Task<bool> CreateUserAsync(CreateUserDto createUser)
        {
            try
            {
                var user = new User
                {
                    RoleId = createUser.RoleId,
                    LastName = createUser.LastName,
                    Name = createUser.Name,
                    Patronymic = createUser.Patronymic,
                    Login = createUser.Login,
                    PasswordHash = createUser.PasswordHash,
                };

                if(user == null) 
                    return false;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException)
            {
                throw;
            }
        }
        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                bool isAssigned = await _context.ForestPlots.AnyAsync(p => p.UserId == userId);
                if (isAssigned)
                    return false;

                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (user == null)
                    return false;

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException)
            {
                throw;
            }
        }
    }
}
