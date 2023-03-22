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
        private readonly SellerService _sellerService;

        public SellerController(UserService userService, SellerService sellerService)
        {
            _userService = userService;
            _sellerService = sellerService;
        }

        /// <summary>Cria um novo usuário com acesso do tipo vendedor.</summary>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserPostRequest request)
            => ConvertData(await _userService.CreateAsync(request, AccessLevelType.Seller));

        /// <summary>Define o nível de acesso de um usuário como vendedor.</summary>
        [HttpPost("level/{sellerUid}")]
        public async Task<IActionResult> SetSeller(string sellerUid)
            => ConvertData(await _userService.SetAsSeller(sellerUid));

        /// <summary>Publica um produto ou anúncio de um vendedor na plataforma.</summary>
        [HttpPost("product")]
        public async Task<IActionResult> PublishProduct([FromBody] SellerPostProductRequest request)
            => ConvertData(await _sellerService.PublishProductAsync(request));

        /// <summary>Publica um produto ou anúncio de um vendedor na plataforma.</summary>
        [HttpGet("product/all/{sellerUid}")]
        public async Task<IActionResult> GetAllProduct(string sellerUid)
            => ConvertData(await _sellerService.GetAllProductsAsync(sellerUid));
    }
}
