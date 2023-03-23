using Ploomes.Application.Contracts;
using Ploomes.Application.Repositories;
using Ploomes.Application.Shared;

namespace Ploomes.Application.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IResultData> GetAll(ProductGetAllFilterRequest request)
        {
            var products = await _productRepository.GetAllAsync(request.Page, request.PageSize);

            return ResultData.Ok(products.Select(p => new ProductResponse(p)));
        }
    }
}
