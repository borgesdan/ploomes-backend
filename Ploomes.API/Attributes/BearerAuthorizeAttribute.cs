using Microsoft.AspNetCore.Authorization;

namespace Ploomes.API.Attributes
{
    /// <summary>
    /// Atributo customizado para o esquema Bearer.
    /// </summary>
    public class BearerAuthorizeAttribute : AuthorizeAttribute
    {
        public BearerAuthorizeAttribute()
        {
            base.AuthenticationSchemes = "Bearer";
        }
    }
}
