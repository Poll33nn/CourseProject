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
                return NotFound("Не удалось получить список участков!");

            return forestPlots;
        }

        // PUT: api/ForestPlots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutForestPlot(UpdateForestPlotDto forestPlotDto)
        {
            var forestPlot = await _service.UpdateForestPlotAsync(forestPlotDto);
            if (forestPlot)
                return Ok("Информаия об участке успешно обнавлена!");

            return NotFound("Такого участка не существует!");
        }

        // POST: api/ForestPlots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ForestPlot>> PostForestPlot(CreateForestPlotDto forestPlotDto)
        {
            var createForestPlot = await _service.CreateForestPlotAsync(forestPlotDto);
            if (createForestPlot)
                return Created();

            return BadRequest("Ошибка при создании участка!");
        }

        // DELETE: api/ForestPlots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForestPlot(int id)
        {
            var forestPlot = await _service.DeleteForestPlotAsync(id);
            if (forestPlot)
                return Ok("Участок успешно удален");
            return NotFound("Такого участка не существует!");
        }
    }
}
