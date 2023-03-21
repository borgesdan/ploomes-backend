using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Ploomes.API.Extensions
{
    public static class ServiceCollectionSwagger
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, AssemblyName applicationName, bool useAuthorization)
        {
            AssemblyName applicationName2 = applicationName;
            services.AddSwaggerGen(delegate (SwaggerGenOptions options)
            {
                SetupSwaggerDoc(options, applicationName2);
                if (useAuthorization)
                {
                    SetupSwaggerAuthorizationDoc(options);
                }

                string path = applicationName2.Name + ".xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, path));
                //options.OperationFilter<CustomErrorsOperationFilter>(Array.Empty<object>());
                //options.SchemaFilter<CustomDefinitionSchemaFilter>(Array.Empty<object>());
            });
            return services;
        }

        private static void SetupSwaggerDoc(SwaggerGenOptions options, AssemblyName applicationName)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = GetSwaggerDocTitle(applicationName) + " APIs",
                Description = "Documentação detalhada para utilização das APIs REST disponíveis no projeto " + applicationName.Name,
                Contact = new OpenApiContact
                {
                    Email = "borges_santos89@hotmail.com",
                    Name = "Danilo Borges",
                    Url = new Uri("http://www.dandev.com.br"),
                }
            });
        }

        private static void SetupSwaggerAuthorizationDoc(SwaggerGenOptions options)
        {
            OpenApiSecurityScheme openApiSecurityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Por favor insira no campo o token JWT com Bearer (BEARER <mytoken>).",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "oauth2"
            };
            options.AddSecurityDefinition("Bearer", openApiSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                openApiSecurityScheme,
                Array.Empty<string>()
            } });
        }

        private static string GetSwaggerDocTitle(AssemblyName applicationName)
        {
            return (applicationName?.Name ?? string.Empty)!.Split(".").First();
        }
    }
}
