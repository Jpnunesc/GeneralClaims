using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Business.Interfaces.Services;
using Business.IO.Users;
using Newtonsoft.Json;
using System;
using Business.IO;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ITokenService _tokenService;

        public UsersController(IUserService service, ITokenService tokenService)
        {
            _service = service;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserAuthView model)
        {
            var user = await _service.Get(model.Nome, model.Senha);

            if (user == null)
                return new { success = false, message = "Usuário ou senha inválidos" };

            var token = _tokenService.GenerateToken(user);

            user.Senha = "";

            return new
            {
                success = true,
                message = "Seja bem-vindo!",
                user = user,
                token = token
            };
        }

    }
}