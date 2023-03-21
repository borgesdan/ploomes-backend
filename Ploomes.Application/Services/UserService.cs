using Ploomes.Application.Contracts;
using Ploomes.Application.Data.Entities.Sql;
using Ploomes.Application.Data.Shared;
using Ploomes.Application.Errors;
using Ploomes.Application.Helpers;
using Ploomes.Application.Repositories;
using Ploomes.Application.Shared;
using Ploomes.Application.Validations;
using System.Net;

namespace Ploomes.Application.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>Cria um novo usuário com um tipo de acesso específico e seu perfil de pessoa.</summary>
        public async Task<IResultData> CreateAsync(UserPostRequest request, AccessLevelType accessLevel)
        {
            var validation = new UserCreateValidator(request);

            if (!validation.Validate())
                return ResultData.Error(validation.Errors.First());

            var result = await CreateTransactionAsync(request, accessLevel);

            if (!result.Succeeded)
                return result;

            var user = result.GetData<UserEntity>();

            return ResultData.Ok(new UserPostResponse(user));
        }

        private async Task<IResultData> CreateTransactionAsync(UserPostRequest request, AccessLevelType accessLevel)
        {
            var context = _userRepository.Context;
            using var transaction =  context.Database.BeginTransaction();

            try
            {
                var user = new UserEntity
                {
                    AccessLevel = accessLevel,
                    CreationDate = DateTime.UtcNow,
                    PrimaryLogin = request.Email,
                    Password = SecurityHelper.CreateHash(request.Password),
                };

                await _userRepository.CreateAsync(user);

                var person = new PersonEntity
                {
                    Name = request.UserName,
                    UserId = user.Id,
                };

                await _userRepository.CreatePersonAsync(person);

                transaction.Commit();

                return ResultData.Ok(user);
            }
            catch
            {
                transaction.Rollback();
            }

            return ResultData.InternalError(InternalError.UserCreateTransaction.Value);
        }

        /// <summary>Define o nível de acesso de um usuário existente como nível de vendedor.</summary>
        public async Task<IResultData> SetAsSeller(Guid uid)
        {
            var user = await _userRepository.GetByUidAsync(uid);

            if (user == null)
                return ResultData.Error("");

            if (user.AccessLevel == AccessLevelType.Buyer)
                return new ResultData(true, null, HttpStatusCode.NoContent);

            user.AccessLevel = AccessLevelType.Seller;
            await _userRepository.Update(user);

            return ResultData.Ok();
        }

        /// <summary>Obtém um usuário por seu Uid.</summary>
        public async Task<IResultData> GetByUid(Guid uid)
        {
            var user = await _userRepository.GetByUidAsync(uid, true);

            if (user == null)
                return ResultData.Error("");

            return ResultData.Ok(new UserGetByUidResponse(user));
        }
    }
}
