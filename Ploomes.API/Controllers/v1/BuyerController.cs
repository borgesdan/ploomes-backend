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
        /// Exemplo de requisição:
        /// <code>
        ///{
        ///  "userName": "Pedro de Alcântara",
        ///  "email": "pedro@email.com",
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
        ///  "message": '"Novo usuário cadastrado com sucesso!'
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
        [HttpPost]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserPostResponse))]        
        public async Task<IActionResult> Create([FromBody] UserPostRequest request)
            => ConvertData(await _userService.CreateAsync(request, AccessLevelType.Buyer));

        /// <summary>Cria um novo pedido de compra.</summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// <code>
        ///{
        ///  "buyerEmail": "pedro@email.com",
        ///  "productUid": "D041E5CE-CB99-4FE2-ABF7-069ECE1E746B"
        ///}
        /// </code>
        /// 
        /// Exemplo de resposta:
        /// <code>
        ///{
        ///     "data:{
        ///       "orderUid": "A2AE0197-FC59-475D-8AA4-ECAB7B206999"
        ///     }
        ///     "succeeded": true,
        ///     "message: 'Pedido realizado com sucesso!',
        ///}
        /// </code>
        /// Retorna somente o identificador do pedido para futura consultas.       
        /// </remarks>        
        [HttpPost("order")]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(BuyerPostCreateOrderResponse))]
        public async Task<IActionResult> CreateOrder([FromBody] BuyerPostCreateOrderRequest request)
            => ConvertData(await _buyerService.CreateOrder(request));

        /// <summary>Obtém todos os pedidos do comprador ao informar seu email.</summary>
        /// <remarks>
        /// Exemplo de resposta:
        /// <code>
        ///{
        ///  "data": [
        ///    {
        ///      "number": 1,
        ///      "orderUid": "4175430a-18ba-4747-8c4f-2d5004f91336",
        ///      "productUid": "d041e5ce-cb99-4fe2-abf7-069ece1e746b",
        ///      "productTitle": "Calça Masculina Jeans",
        ///      "productCount": 1
        ///    },
        ///    {
        ///      "number": 2,
        ///      "orderUid": "e0f7d288-f8fd-423e-8110-c9b0e0978e52",
        ///      "productUid": "d041e5ce-cb99-4fe2-abf7-069ece1e746b",
        ///      "productTitle": "Bermuda Masculina Alta Estação",
        ///      "productCount": 1
        ///    }
        ///  ],
        ///  "succeeded": true,
        ///  "message": null
        ///}
        ///</code>
        /// </remarks>
        [HttpGet("{buyerEmail}/order/all")]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(BuyerGetAllOrdersResponse))]
        public async Task<IActionResult> GetAllOrders(string buyerEmail)
            => ConvertData(await _buyerService.GetAllOrders(buyerEmail));

        /// <summary>Define o perfil como vendedor para a plataforma.</summary>
        /// <remarks>
        /// Exemplo de resposta:
        /// <code>
        ///{
        ///  "succeeded": true,
        ///  "message": 'Usuário verificado como vendedor',
        ///}
        ///</code>
        ///
        /// Ocorrerá erro caso o usuário não seja encontrado pelo email informado.
        /// </remarks>
        [HttpPatch("{buyerEmail}/upgrade")]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData))]
        public async Task<IActionResult> SetSeller(string buyerEmail)
            => ConvertData(await _userService.SetUserAsSeller(buyerEmail));


    }
}