using Azure.Core;
using Ploomes.Application.Contracts;
using Ploomes.Application.Data.Entities.Sql;
using Ploomes.Application.Data.Shared;
using Ploomes.Application.Errors;
using Ploomes.Application.Repositories;
using Ploomes.Application.Shared;
using Ploomes.Application.Validations;

namespace Ploomes.Application.Services
{
    public class SellerService
    {
        private readonly UserRepository _userRepository;
        private readonly ProductRepository _productRepository;

        public SellerService(UserRepository userRepository, ProductRepository productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        /// <summary>Publica um novo produto ou anúncio de um vendedor.</summary>
        public async Task<IResultData> PublishProductAsync(SellerPostProductRequest request)
        {
            var validation = new SellerPublishProductValidator(request);

            if(!validation.Validate())
                return ResultData.Error(validation.Errors.First());

            var user = await _userRepository.GetByEmail(request.SellerEmail);

            if (user == null)
                return ResultData.Error(AppError.User.NotFound.Message);

            if (!AccessLevelReader.IsSeller(user.AccessLevel))
                return ResultData.Error(AppError.User.InvalidPermission.Message);

            var product = new ProductEntity
            {
                Title = request.Title,
                Price = request.Price,                
                Description = request.Description,
                SellerId = user.Id
            };

            await _productRepository.Create(product);

            return ResultData.Ok(new SellerPostProductResponse(product.Uid));
        }

        /// <summary>Obtém todos os produtos de um vendedor.</summary>
        public async Task<IResultData> GetAllProductsAsync(string sellerEmail)
        {
            var user = await _userRepository.GetByEmail(sellerEmail);

            if (user == null)
                return ResultData.Error(AppError.User.NotFound.Message);

            if (!AccessLevelReader.IsSeller(user.AccessLevel))
                return ResultData.Error(AppError.User.InvalidPermission.Message);

            var products = await _productRepository.GetAllAsync(user.Id);

            return ResultData.Ok(new SellerGetAllProductsResponse()
            {
                Products = products.Select(p => new ProductResponse(p)).ToList(),
            });
        }

        /// <summary>Muda a visibilidade de um produto para que não seja exibido na plataforma.</summary>
        public async Task<IResultData> HideProduct(SellerPatchHideProduct request)
        {
            var user = await _userRepository.GetByEmail(request.SellerEmail);

            if (user == null)
                return ResultData.Error(AppError.User.NotFound.Message);

            if (!AccessLevelReader.IsSeller(user.AccessLevel))
                return ResultData.Error(AppError.User.InvalidPermission.Message);

            var product = await _productRepository.GetByUidAsync(request.ProductUid);

            if (product == null)
                return ResultData.Error(AppError.Product.NotFound.Message);

            product.Status = EntityStatus.Hidden;
            await _productRepository.Update(product);

            return ResultData.Ok();
        }
    }
}