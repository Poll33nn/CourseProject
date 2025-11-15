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
            if (string.IsNullOrEmpty(loginDto.Login))
                return BadRequest("Логин не указан");
            if (string.IsNullOrEmpty(loginDto.PasswordHash))
                return BadRequest("Пароль не указан");

            var user = await _accountService.LoginUser(loginDto);
            if (user == null)
                return BadRequest("Логин или пароль не верный");
            return Ok(_tokenService.GenerateToken(user));
        }
    }
}
