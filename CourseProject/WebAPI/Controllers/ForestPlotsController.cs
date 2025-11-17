using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.DTO_s;
using ServiceLayer.Models;
using ServiceLayer.Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForestPlotsController : ControllerBase
    {
        private readonly ForestPlotService _service;
        public ForestPlotsController(ForestPlotService service)
        {
            _service = service;
        }

        // GET: api/ForestPlots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForestPlotDto>>> GetForestPlots()
        {
            var forestPlots = await _service.GetForestPlotsAsync();
            if (forestPlots == null) 
                return NotFound();

            return forestPlots;
        }

        //// GET: api/ForestPlots/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ForestPlot>> GetForestPlot(int id)
        //{
        //    var forestPlot = await _context.ForestPlots.FindAsync(id);

        //    if (forestPlot == null)
        //    {
        //        return NotFound();
        //    }

        //    return forestPlot;
        //}

        // PUT: api/ForestPlots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutForestPlot(int id, ForestPlot forestPlot)
        {
            if (id != forestPlot.PlotId)
            {
                return BadRequest();
            }

            _context.Entry(forestPlot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ForestPlotExists(id))
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

        // POST: api/ForestPlots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ForestPlot>> PostForestPlot(ForestPlot forestPlot)
        {
            _context.ForestPlots.Add(forestPlot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetForestPlot", new { id = forestPlot.PlotId }, forestPlot);
        }

        // DELETE: api/ForestPlots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForestPlot(int id)
        {
            var forestPlot = await _context.ForestPlots.FindAsync(id);
            if (forestPlot == null)
            {
                return NotFound();
            }

            _context.ForestPlots.Remove(forestPlot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ForestPlotExists(int id)
        {
            return _context.ForestPlots.Any(e => e.PlotId == id);
        }
    }
}
