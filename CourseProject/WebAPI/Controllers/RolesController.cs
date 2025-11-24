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
    public class RolesController : ControllerBase
    {
        private readonly RoleService _service = new();
        // GET: api/Roles
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetTreeTypeName()
        {
            var roleName = await _service.GetRoleNameAsync();
            if (roleName == null)
                return NotFound("Не удалось получить список пользователей!");

            return roleName;
        }
    }
}
