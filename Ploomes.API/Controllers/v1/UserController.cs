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
        /// Obtém um usuário por seu email.
        /// </summary>
        /// <remarks>        
        /// Exemplo de resposta:
        /// <code>
        ///{
        ///  "succeeded": true,
        ///  "message": null,
        ///  "data": {
        ///    "uid": "3f400aaa-7dab-4900-98ed-efd28bfa91ad",
        ///    "email": "user@email.com",
        ///    "accessLevel": 1,
        ///    "creationDate": "2023-03-22T21:15:23.748Z",
        ///    "name": "User Name dos Santos"
        ///  }
        ///}        
        /// </code>
        /// Você precisa estar logado para utilizar esse endpoint.
        /// </remarks>
        [HttpGet("{userEmail}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData<UserGetByUidResponse>))]        
        public async Task<IActionResult> GetByUid(string userEmail)
            => ConvertData(await _userService.GetByEmail(userEmail));

        /// <summary>Executa o login no sistema de um usuário cadastrado.</summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// <code>
        ///{
        ///  "email": "string",
        ///  "password": "string"
        ///}
        ///</code>
        ///
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
