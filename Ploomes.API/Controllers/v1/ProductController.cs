using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ploomes.Application.Contracts;
using Ploomes.Application.Services;

namespace Ploomes.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : DefaultController
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Obtém todos os produtos cadastrados no sistema com paginação.
        /// </summary>
        /// <remarks>        
        /// Exemplo de requisição:
        /// <code>
        ///{
        ///  "page": 1,
        ///  "pageSize": 10
        ///}   
        /// </code>
        /// 
        /// Nesse exemplo busca-se 10 produtos na primeira página.
        /// 
        /// Exemplo de resposta:
        /// <code>
        ///{
        ///  "data": [
        ///    {
        ///      "uid": "d041e5ce-cb99-4fe2-abf7-069ece1e746b",
        ///      "title": "Bermudas masculinas",
        ///      "price": 60,
        ///      "description": "bermudas masculinas"
        ///    },
        ///    {
        ///      "uid": "dccb2a96-d3b3-44d8-aede-2fc12612e917",
        ///      "title": "Calças masculinas",
        ///      "price": 60,
        ///      "description": "calças masculinas"
        ///    }
        ///  ],
        ///  "succeeded": true,
        ///  "message": null
        ///}
        ///</code>
        /// </remarks>
        [HttpPost("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromBody] ProductGetAllFilterRequest request)
            => ConvertData(await _productService.GetAll(request));
    }
}