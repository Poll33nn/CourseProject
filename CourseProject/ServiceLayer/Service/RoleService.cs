using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.DTO_s;

namespace ServiceLayer.Service
{
    public class RoleService
    {
        private readonly ForestryContext _context = new();
        public async Task<List<RoleDto>> GetRoleNameAsync()
        {
            try
            {
                var roleNames = await _context.Roles.ToListAsync();

                if (roleNames == null)
                    return null;

                return roleNames.Select(roleName => new RoleDto
                {
                    RoleName = roleName.Name,
                }).ToList();
            }
            catch (DbException)
            {
                throw;
            }
        }
    }
}
