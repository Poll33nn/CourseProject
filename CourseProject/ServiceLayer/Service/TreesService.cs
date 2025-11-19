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
    public class TreesService
    {
        private readonly ForestryContext _context = new();

        public async Task<List<TreeTypeDto>> GetTreeTypeNameAsync()
        {
            var treeTypes = await _context.TreeTypes.ToListAsync();
            return treeTypes.Select(treeType => new TreeTypeDto
            {
                TreeTypeId = treeType.TreeTypeId,
                TreeType = treeType.Name,
            }).ToList();
        }
    }
}
