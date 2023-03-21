using Ploomes.Application.Contracts;
using Ploomes.Application.Data.Shared;
using Ploomes.Application.Errors;

namespace Ploomes.Application.Validations
{
    /// <summary>Classe auxiliar para validação dos dados de criação do usuário.</summary>
    public class UserCreateValidator : FlowValidator<UserPostRequest>
    {
        public UserCreateValidator(UserPostRequest request) : base(request)
        {
            IsNull()
                .AddError(AppError.NoDataHasBeenReported.Message)
            .IsNull(request.UserName)
                .AddError(AppError.User.NameNotInformed.Message)
            .IsNull(request.Email)
                .AddError(AppError.User.EmailNotInformed.Message)
            .IsNull(request.Password)
                .AddError(AppError.User.PasswordNotInformed.Message);            
        }
    }
}
