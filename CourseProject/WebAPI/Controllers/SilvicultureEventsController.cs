using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class SilvicultureEventsController : ControllerBase
    {
        private readonly EventService _service = new();

        // GET: api/SilvicultureEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForestEventReportDto>>> GetAllSilvicultureEvents()
        {
            var forestryEvents = await _service.GetAllSilvicultureEventAsync();
            if (forestryEvents == null)
                return NotFound("Не удалось получить список участков!");

            return forestryEvents;
        }

        // GET: api/SilvicultureEvents/5
        [HttpGet("{PlotId}")]
        public async Task<ActionResult<IEnumerable<ForestEventReportDto>>> GetPlotSilvicultureEvents(int PlotId)
        {
            var forestryEvents = await _service.GetPlotSilvicultureEventAsync(PlotId);
            if (forestryEvents == null)
                return NotFound("Не удалось получить список участков!");

            return forestryEvents;
        }

        // POST: api/SilvicultureEvents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SilvicultureEventDto>> PostSilvicultureEvent(CreateSilvicultureEventDto silvicultureEvent)
        {
            var createEvent = await _service.CreateSilvicultureEventAsync(silvicultureEvent);
            if (createEvent)
                return Created();

            return BadRequest("Ошибка при создании участка!");
        }
    }
}
