using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO_s;
using ServiceLayer.Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventTypesController : ControllerBase
    {

        private readonly EventService _service;
        public EventTypesController(EventService service)
        {
            _service = service;
        }

        // GET: api/EventTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventTypeDto>>> GetEventTypeName()
        {
            var eventName = await _service.GetEventTypeNameAsync();
            if (eventName == null)
                return BadRequest("Не удалось получить список имен мероприятий!");

            return eventName;
        }
    }
}
