using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ploomes.Application.Services;

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

        /// <summary>Obtém um usuário por seu uid.</summary>
        [HttpGet("{uid}")]
        public async Task<IActionResult> GetByUid(Guid uid)
            => ConvertData(await _userService.GetByUid(uid));
    }
}
