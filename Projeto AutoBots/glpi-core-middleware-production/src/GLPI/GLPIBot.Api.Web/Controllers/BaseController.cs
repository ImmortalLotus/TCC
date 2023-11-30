using Microsoft.AspNetCore.Mvc;

namespace GLPIBot.Api.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult Json(dynamic data)
        {
            return Ok(data);
        }

        protected IActionResult Success(string message)
        {
            return Ok(new
            {
                success = true,
                message = message
            });
        }

        protected IActionResult Fail(string message)
        {
            return BadRequest(new
            {
                success = false,
                message,
            });
        }

        protected IActionResult SessionFail()
        {
            return BadRequest(new
            {
                success = false,
                message="Erro ao buscar a Sessão no GLPI.",
            });
        }
    }
}
