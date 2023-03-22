using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Ploomes.Application.Contracts;
using Ploomes.Application.Data.Entities.Sql;
using Ploomes.Application.Data.Shared;
using Ploomes.Application.Errors;
using Ploomes.Application.Helpers;
using Ploomes.Application.Repositories;
using Ploomes.Application.Shared;
using Ploomes.Application.Validations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Ploomes.Application.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        /// <summary>Cria um novo usuário com um tipo de acesso específico e seu perfil de pessoa.</summary>
        public async Task<IResultData> CreateAsync(UserPostRequest request, AccessLevelType accessLevel)
        {
            var validation = new UserCreateValidator(request);

            if (!validation.Validate())
                return ResultData.Error(validation.Errors.First());

            var emailUser = await _userRepository.Get(u => u.Email == request.Email);

            if (emailUser != null)
                return ResultData.Error(AppError.User.AlreadyExistingEmail.Message);

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
                    Email = request.Email,
                    Password = SecurityHelper.CreateHash(request.Password),
                };

                await _userRepository.CreateAsync(user);

                var person = new PersonEntity
                {
                    FullName = request.UserName,
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
        public async Task<IResultData> SetAsSeller(string uid)
        {
            var user = await _userRepository.GetByUidAsync(uid);

            if (user == null)
                return ResultData.Error(AppError.User.NotFound);

            if (user.AccessLevel == AccessLevelType.Buyer)
                return new ResultData(true, null, HttpStatusCode.NoContent);

            user.AccessLevel = AccessLevelType.Seller;
            await _userRepository.Update(user);

            return ResultData.Ok();
        }

        /// <summary>Obtém os dados de um usuário por seu Uid.</summary>
        public async Task<IResultData> GetByUid(string uid)
        {
            var user = await _userRepository.GetByUidAsync(uid, true);

            if (user == null)
                return ResultData.Error(AppError.User.NotFound);

            return ResultData.Ok(new UserGetByUidResponse(user));
        }

        /// <summary>Aplica login do usuário.</summary>
        public async Task<IResultData> Login(UserLoginRequest request)
        {
            var validator = new UserLoginValidator(request);

            if (!validator.Validate())
                return ResultData.Error(validator.Errors.First());

            var passwordHash = SecurityHelper.CreateHash(request.Password);

            var user = await _userRepository.GetByLogin(request.Email, passwordHash);

            if (user == null)
                return ResultData.Error(AppError.User.InvalidLogin.Message);

            var tokenRequest = new UserTokenRequest
            {
                UserUid = user.Uid,
                AccessLevel = user.AccessLevel,
                Email = request.Email
            };

            var tokenResult = GenerateToken(tokenRequest);
            var tokenResponse = tokenResult.GetData<UserTokenResponse>();

            return ResultData.Ok(tokenResponse);
        }

        /// <summary>Obtém um token JWT para login do usuário.</summary>
        public IResultData GenerateToken(UserTokenRequest request)
        {
            //Dados obtidos do arquivo de configuração appsettings.json
            var jwtKeyValue = _configuration["Jwt:Key"];
            var tokenExpireValue = _configuration["TokenConfiguration:ExpireHours"];
            var tokenIssuerValue = _configuration["TokenConfiguration:Issuer"];
            var tokenAudienceValue = _configuration["TokenConfiguration:Audience"];

            if (jwtKeyValue == null || tokenExpireValue == null
                || tokenIssuerValue == null || tokenAudienceValue == null)
            {
                return ResultData.InternalError(InternalError.ConfigurationAccessJwtAccess.Value);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKeyValue));
            var expiration = DateTime.UtcNow.AddHours(double.Parse(tokenExpireValue));
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, ((int)request.AccessLevel).ToString()),
                new Claim(ClaimTypes.NameIdentifier, request.UserUid.ToString()),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = tokenAudienceValue,
                Issuer = tokenIssuerValue,
                Expires = expiration,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var response = new UserTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };

            return ResultData.Ok(response);
        }
    }
}
