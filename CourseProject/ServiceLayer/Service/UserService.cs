using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class UserService
    {
        private readonly ForestryContext _context = new();

        public async Task<List<UserDto>> GetAllResposibleAsync()
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
    }
}
