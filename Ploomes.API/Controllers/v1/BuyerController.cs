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
    public class BuyerController : DefaultController
    {
        private readonly UserService _userService;
        private readonly BuyerService _buyerService;

        public BuyerController(UserService userService, BuyerService buyerService)
        {
            _userService = userService;
            _buyerService = buyerService;
        }

        /// <summary>Cria um novo usuário com acesso do tipo comprador.</summary>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserPostRequest request)
            => ConvertData(await _userService.CreateAsync(request, AccessLevelType.Buyer));

        /// <summary>Cria um pedido de compra de um produto.</summary>
        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] BuyerPostCreateOrderRequest request)
            => ConvertData(await _buyerService.CreateOrder(request));
    }
}