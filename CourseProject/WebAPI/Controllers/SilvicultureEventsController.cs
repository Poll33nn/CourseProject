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
    public class SilvicultureEventsController : ControllerBase
    {
        private readonly ForestryContext _context;

        public SilvicultureEventsController(ForestryContext context)
        {
            _context = context;
        }

        // GET: api/SilvicultureEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SilvicultureEvent>>> GetSilvicultureEvents()
        {
            return await _context.SilvicultureEvents.ToListAsync();
        }

        // GET: api/SilvicultureEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SilvicultureEvent>> GetSilvicultureEvent(int id)
        {
            var silvicultureEvent = await _context.SilvicultureEvents.FindAsync(id);

            if (silvicultureEvent == null)
            {
                return NotFound();
            }

            return silvicultureEvent;
        }

        // PUT: api/SilvicultureEvents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSilvicultureEvent(int id, SilvicultureEvent silvicultureEvent)
        {
            if (id != silvicultureEvent.EventId)
            {
                return BadRequest();
            }

            _context.Entry(silvicultureEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SilvicultureEventExists(id))
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

        // POST: api/SilvicultureEvents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SilvicultureEvent>> PostSilvicultureEvent(SilvicultureEvent silvicultureEvent)
        {
            _context.SilvicultureEvents.Add(silvicultureEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSilvicultureEvent", new { id = silvicultureEvent.EventId }, silvicultureEvent);
        }

        // DELETE: api/SilvicultureEvents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSilvicultureEvent(int id)
        {
            var silvicultureEvent = await _context.SilvicultureEvents.FindAsync(id);
            if (silvicultureEvent == null)
            {
                return NotFound();
            }

            _context.SilvicultureEvents.Remove(silvicultureEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SilvicultureEventExists(int id)
        {
            return _context.SilvicultureEvents.Any(e => e.EventId == id);
        }
    }
}
