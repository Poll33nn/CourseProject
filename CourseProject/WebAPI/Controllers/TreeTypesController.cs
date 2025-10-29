using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreeTypesController : ControllerBase
    {
        private readonly ForestryContext _context;

        public TreeTypesController(ForestryContext context)
        {
            _context = context;
        }

        // GET: api/TreeTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreeType>>> GetTreeTypes()
        {
            return await _context.TreeTypes.ToListAsync();
        }

        // GET: api/TreeTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TreeType>> GetTreeType(int id)
        {
            var treeType = await _context.TreeTypes.FindAsync(id);

            if (treeType == null)
            {
                return NotFound();
            }

            return treeType;
        }

        // PUT: api/TreeTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTreeType(int id, TreeType treeType)
        {
            if (id != treeType.TreeTypeId)
            {
                return BadRequest();
            }

            _context.Entry(treeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreeTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TreeTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TreeType>> PostTreeType(TreeType treeType)
        {
            _context.TreeTypes.Add(treeType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTreeType", new { id = treeType.TreeTypeId }, treeType);
        }

        // DELETE: api/TreeTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreeType(int id)
        {
            var treeType = await _context.TreeTypes.FindAsync(id);
            if (treeType == null)
            {
                return NotFound();
            }

            _context.TreeTypes.Remove(treeType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TreeTypeExists(int id)
        {
            return _context.TreeTypes.Any(e => e.TreeTypeId == id);
        }
    }
}
