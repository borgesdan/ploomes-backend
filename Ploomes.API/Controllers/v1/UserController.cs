using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ploomes.Application.Contracts;
using Ploomes.Application.Services;
using Ploomes.Application.Shared;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Ploomes.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : DefaultController
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtém um usuário por seu uid.
        /// </summary>
        /// <remarks>
        /// Exemplo de resposta:
        /// <code>
        ///{
        ///  "succeeded": true,
        ///  "message": null,
        ///  "data": {
        ///    "uid": "A52AF318-BD4F-48F3-ABE6-26F7B13F151D",
        ///    "email": "user@email.com",
        ///    "accessLevel": 1,
        ///    "creationDate": "2023-03-22T21:15:23.748Z",
        ///    "name": "User Name dos Santos"
        ///  }
        ///}
        /// </code>
        /// Posteriomente a chamada desse endpoint deve ser restrita para administradores.
        /// </remarks>
        [HttpGet("{uid}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData<UserGetByUidResponse>))]        
        public async Task<IActionResult> GetByUid(string uid)
            => ConvertData(await _userService.GetByUid(uid));

        /// <summary>Executa o login no sistema de um usuário cadastrado.</summary>
        /// <remarks>
        /// Exemplo de resposta:
        /// <code>
        ///{
        ///     "succeeded": true,
        ///     "message": null,
        ///     "data": 
        ///     {
        ///         "token": "eyGhbYcUOiHIUmm1NkjIs7nR56CIpIçpXVjJ9...",
        ///         "expiration": "2023-03-22T21:07:36.110Z",
        ///     }
        ///}
        /// </code>
        /// No Swagger utilize o token no botão Authorize, iniciando a string com 'bearer': "Bearer eyGhbYcUOiHIUmm1NkjIs7nR56CIpIçpXVjJ9..."
        /// </remarks>
        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData<UserTokenResponse>))]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
            => ConvertData(await _userService.Login(request));
    }
}
