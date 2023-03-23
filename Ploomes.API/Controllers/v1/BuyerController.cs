using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ploomes.Application.Contracts;
using Ploomes.Application.Data.Shared;
using Ploomes.Application.Services;
using Ploomes.Application.Shared;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

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

        /// <summary>Cria um novo usuário para acesso somente à compras na plataforma.</summary>
        /// <remarks>
        /// Exemplo de resposta:
        /// <code>
        ///{
        ///  "uid": "B4FCE7CA-35C2-48E3-8B60-3A6DB07BEB99",
        ///}
        /// </code>
        /// </remarks>
        [AllowAnonymous]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserPostResponse))]        
        public async Task<IActionResult> Create([FromBody] UserPostRequest request)
            => ConvertData(await _userService.CreateAsync(request, AccessLevelType.Buyer));

        /// <summary>Cria um novo pedido de compra.</summary>
        /// <remarks>
        /// Exemplo de resposta:
        /// <code>
        ///{
        ///  "orderUid": "A2AE0197-FC59-475D-8AA4-ECAB7B206999",
        ///}
        /// </code>
        /// Retorna somente o identificador do pedido para futura solicitações.
        /// </remarks>
        [HttpPost("order")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(BuyerPostCreateOrderResponse))]
        public async Task<IActionResult> CreateOrder([FromBody] BuyerPostCreateOrderRequest request)
            => ConvertData(await _buyerService.CreateOrder(request));

        /// <summary>Define o perfil como vendedor para a plataforma.</summary>
        /// <remarks>
        /// Exemplo de resposta:
        /// <code>
        ///{
        ///  "succeeded": true,
        ///  "message": 'Usuário verificado como vendedor',
        ///}
        ///</code>
        /// </remarks>
        [HttpPatch("{buyerId}/upgrade")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData<IResultData>))]
        public async Task<IActionResult> SetSeller(string buyerId)
            => ConvertData(await _userService.SetUserAsSeller(buyerId));
    }
}