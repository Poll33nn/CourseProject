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
    public class TreesNumbersController : ControllerBase
    {
        private readonly ForestryContext _context;

        public TreesNumbersController(ForestryContext context)
        {
            _context = context;
        }

        // GET: api/TreesNumbers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreesNumber>>> GetTreesNumbers()
        {
            return await _context.TreesNumbers.ToListAsync();
        }

        // GET: api/TreesNumbers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TreesNumber>> GetTreesNumber(int id)
        {
            var treesNumber = await _context.TreesNumbers.FindAsync(id);

            if (treesNumber == null)
            {
                return NotFound();
            }

            return treesNumber;
        }

        // PUT: api/TreesNumbers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTreesNumber(int id, TreesNumber treesNumber)
        {
            if (id != treesNumber.PlotId)
            {
                return BadRequest();
            }

            _context.Entry(treesNumber).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreesNumberExists(id))
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

        // POST: api/TreesNumbers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TreesNumber>> PostTreesNumber(TreesNumber treesNumber)
        {
            _context.TreesNumbers.Add(treesNumber);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TreesNumberExists(treesNumber.PlotId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTreesNumber", new { id = treesNumber.PlotId }, treesNumber);
        }

        // DELETE: api/TreesNumbers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreesNumber(int id)
        {
            var treesNumber = await _context.TreesNumbers.FindAsync(id);
            if (treesNumber == null)
            {
                return NotFound();
            }

            _context.TreesNumbers.Remove(treesNumber);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TreesNumberExists(int id)
        {
            return _context.TreesNumbers.Any(e => e.PlotId == id);
        }
    }
}
