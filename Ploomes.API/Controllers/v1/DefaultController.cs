using Microsoft.AspNetCore.Mvc;
using Ploomes.Application.Shared;

namespace Ploomes.API.Controllers.v1
{
    /// <summary>
    /// Controle padrão com métodos e propriedades de auxílios.
    /// </summary>
    public abstract class DefaultController : ControllerBase
    {
        /// <summary>
        /// Converte o tipo de dado recebido pelo serviço em um IActionResult válido
        /// a ser enviado como resposta do controle.
        /// </summary>        
        protected IActionResult ConvertData(IResultData resultData)
        {
            if (resultData == null)
                NoContent();

            var result = new ObjectResult(resultData)
            {
                StatusCode = (int)resultData.StatusCode()
            };

            return result;
        }
    }
}
