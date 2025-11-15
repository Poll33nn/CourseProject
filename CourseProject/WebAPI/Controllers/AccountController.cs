using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO_s;
using ServiceLayer.Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly AccountService _accountService;
        public AccountController(TokenService tokenService, AccountService accountService)
        {
            _tokenService = tokenService;  
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _accountService.LoginUser(loginDto);
            if (user == null) 
                return NotFound();

            return Ok(_tokenService.GenerateToken(user));
        }
    }
}
