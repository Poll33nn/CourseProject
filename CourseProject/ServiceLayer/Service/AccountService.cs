using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.DTO_s;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class AccountService
    {
        private readonly ForestryContext _context = new();

        public async Task<UserDto> LoginUser(LoginDto loginDto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(
                    u => u.Login == loginDto.Login && 
                    u.PasswordHash == loginDto.PasswordHash);
            
            if (user == null)
                return null;

            var userDto = new UserDto()
            {
                FullName = user.LastName + " " + user.Name + " " + user.Patronymic,
                RoleName = user.Role.Name,
            };
            return userDto;
        }
    }
}
