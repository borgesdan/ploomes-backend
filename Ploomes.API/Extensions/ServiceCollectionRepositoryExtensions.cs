using Ploomes.Application.Repositories;

namespace Ploomes.API.Extensions
{
    public static class ServiceCollectionRepositoryExtensions
    {
        /// <summary>Adiciona os repositórios da aplicação aos serviços.</summary>
        public static IServiceCollection AddAppRepositories(this IServiceCollection services)
        {
            services.AddScoped<UserRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<SellerRepository>();
            return services;
        }
    }
}
