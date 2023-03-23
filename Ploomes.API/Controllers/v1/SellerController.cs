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

        /// <summary>Cria um novo usuário para acesso de compra e venda na plataforma.</summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// <code>
        ///{
        ///  "userName": "Maria Elizabete de Alcântara",
        ///  "email": "maria@email.com",
        ///  "password": "123456@mudar"
        ///}
        ///</code>
        ///
        /// Todos os campos são obrigatórios.
        ///
        /// Exemplo de resposta:
        /// <code>
        ///{
        ///  "data": {
        ///    "uid": "aa4b0679-c759-41aa-8c92-70329ce4ff0b"
        ///  },
        ///  "succeeded": true,
        ///  "message": null
        ///}
        /// </code>
        /// Retorna o identificador único do usuário cadastrado para futura solicitações.
        ///
        /// Ocorrerá erro na requisição caso:
        /// <code>
        /// 1 - Exista um email cadastrado igual ao informado;
        /// 2 - Alguns dos campos não seja informado.
        /// </code>
        /// </remarks>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserPostRequest request)
            => ConvertData(await _userService.CreateAsync(request, AccessLevelType.Seller));        

        /// <summary>Publica um produto ou anúncio de um vendedor na plataforma.</summary>
        [HttpPost("product")]
        public async Task<IActionResult> PublishProduct([FromBody] SellerPostProductRequest request)
            => ConvertData(await _sellerService.PublishProductAsync(request));

        /// <summary>Obtém todos os produtos de um vendedor.</summary>
        [HttpGet("{sellerUid}/product/all/")]
        public async Task<IActionResult> GetAllProduct(string sellerUid)
            => ConvertData(await _sellerService.GetAllProductsAsync(sellerUid));

        /// <summary>Atualiza a visibilidade de um produto para que não seja exibido na plataforma.</summary>
        [HttpPatch("{sellerUid}/product/hidden/{productUid}")]
        public async Task<IActionResult> HideProduct(string sellerUid, string productUid)
            => ConvertData(await _sellerService.HideProduct(sellerUid, productUid));
    }
}
