﻿using Azure.Core;
using Ploomes.Application.Contracts;
using Ploomes.Application.Data.Entities.Sql;
using Ploomes.Application.Data.Shared;
using Ploomes.Application.Repositories;
using Ploomes.Application.Shared;

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
            var user = await _userRepository.GetByUidAsync(request.SellerUid);

            if (user == null)
                return ResultData.Error("");

            if (!AccessLevelReader.IsSeller(user.AccessLevel))
                return ResultData.Error("");

            var product = new ProductEntity
            {
                Title = request.Title,
                Price = request.Price,
                Discount = request.Discount,
                Description = request.Description,
                SellerId = user.Id
            };

            await _productRepository.Create(product);

            return ResultData.Ok(new SellerPostProductResponse() 
            { 
                ProductUid = product.Uid.ToString() 
            });
        }

        /// <summary>Obtém todos os produtos de um vendedor.</summary>
        public async Task<IResultData> GetAllProductsAsync(string sellerUid)
        {
            var user = await _userRepository.GetByUidAsync(sellerUid);

            if (user == null)
                return ResultData.Error("");

            if (!AccessLevelReader.IsSeller(user.AccessLevel))
                return ResultData.Error("");

            var products = await _productRepository.GetAll(user.Id);

            return ResultData.Ok(new SellerGetAllProductsResponse()
            {
                Products = products.Select(p => new ProductResponse(p)).ToList(),
            });
        }
    }
}