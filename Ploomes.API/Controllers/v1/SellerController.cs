using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ploomes.Application.Contracts;
using Ploomes.Application.Data.Shared;
using Ploomes.Application.Services;

namespace Ploomes.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SellerController : DefaultController
    {
        private readonly UserService _userService;

        public SellerController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>Cria um novo usuário com acesso do tipo vendedor.</summary>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserPostRequest request)
            => ConvertData(await _userService.CreateAsync(request, AccessLevelType.Seller));

        /// <summary>Define o nível de acesso de um usuário como vendedor.</summary>
        [HttpPost("changelevel/{uid}")]
        public async Task<IActionResult> SetSeller(Guid uid)
            => ConvertData(await _userService.SetAsSeller(uid));
    }
}
