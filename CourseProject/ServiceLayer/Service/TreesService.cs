using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.DTO_s;
using ServiceLayer.Models;

namespace ServiceLayer.Service
{
    public class TreesService
    {
        private readonly ForestryContext _context = new();

        public async Task<List<TreeTypeDto>> GetTreeTypeNameAsync()
        {
            var treeTypes = await _context.TreeTypes.ToListAsync();

            if (treeTypes == null)
                return null;

            return treeTypes.Select(treeType => new TreeTypeDto
            {
                TreeTypeId = treeType.TreeTypeId,
                TreeType = treeType.Name,
            }).ToList();
        }
    }
}
