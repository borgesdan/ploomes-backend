﻿using Ploomes.Application.Services;

namespace Ploomes.API.Extensions
{
    public static class ServiceCollectionServiceExtensions
    {
        /// <summary>Adiciona os serviços da aplicação a coleção de serviços.</summary>
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<SellerService>();
            services.AddScoped<BuyerService>();            
            services.AddScoped<ProductService>();            
            return services;
        }
    }
}
