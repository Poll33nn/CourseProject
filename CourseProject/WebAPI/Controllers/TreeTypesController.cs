using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO_s;
using ServiceLayer.Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreeTypesController : ControllerBase
    {
        private readonly TreesService _service = new();
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreeTypeDto>>> GetTreeTypeName()
        {
            var treeType = await _service.GetTreeTypeNameAsync();
            if (treeType == null)
                return NotFound("Не удалось получить список пользователей!");

            return treeType;
        }
    }
}
