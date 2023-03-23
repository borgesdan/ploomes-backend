using Ploomes.Application.Contracts;
using Ploomes.Application.Data.Entities.Sql;
using Ploomes.Application.Data.Shared;
using Ploomes.Application.Errors;
using Ploomes.Application.Repositories;
using Ploomes.Application.Shared;

namespace Ploomes.Application.Services
{
    public class BuyerService
    {
        private readonly ProductRepository _productRepository;
        private readonly UserRepository _userRepository;
        private readonly OrderRepository _orderRepository;

        public BuyerService(
            ProductRepository productRepository,
            UserRepository userRepository,
            OrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IResultData> CreateOrder(BuyerPostCreateOrderRequest request)
        {
            var user = await _userRepository.GetByUidAsync(request.BuyerUid);

            if (user == null)
                return ResultData.Error(AppError.User.NotFound.Message);            

            var product = await _productRepository.GetByUidAsync(request.ProductUid);

            if (product == null)
                return ResultData.Error(AppError.Product.NotFound.Message);

            //Verificação para que o vendedor não compre seu próprio produto
            if(AccessLevelReader.IsSeller(user.AccessLevel) && product.SellerId == user.Id)
                return ResultData.Error(AppError.Seller.CannotBuyHisOwnProduct.Message);

            if (request.ProductCount > product.Stock)
                return ResultData.Error(AppError.Product.StockExceeded.Message);

            var order = new OrderEntity
            {
                BuyerId = user.Id,
                ProductId = product.Id,
                ProductCount = request.ProductCount,
            };

            await _orderRepository.Create(order);

            return ResultData.Ok(new BuyerPostCreateOrderResponse(order));
        }

        public async Task<IResultData> GetAllOrders(string buyerUid)
        {
            var user = await _userRepository.GetByUidAsync(buyerUid);

            if (user == null)
                return ResultData.Error(AppError.User.NotFound.Message);

            var orders = await _orderRepository.GetAllByBuyerIdAsync(user.Id, true);

            return ResultData.Ok(orders.Select(o => new BuyerGetAllOrdersResponse(o)));
        }
    }
}