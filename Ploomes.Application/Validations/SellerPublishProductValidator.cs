using Ploomes.Application.Contracts;
using Ploomes.Application.Errors;

namespace Ploomes.Application.Validations
{
    public class SellerPublishProductValidator : FlowValidator<SellerPostProductRequest>
    {
        public SellerPublishProductValidator(SellerPostProductRequest request) : base(request)
        {
            IsNull()
                .AddError(AppError.NoDataHasBeenReported.Message)
            .IsNull(request.Title)
                .AddError(AppError.Product.TitleNotInformed.Message)
            .IsNull(request.Description)
                .AddError(AppError.Product.DescriptionNotInformed.Message)
            .IsZeroOrNegative(request.Price)
                .AddError(AppError.Product.PriceCannotBeZeroOrNegative.Message);           
            
        }
    }
}