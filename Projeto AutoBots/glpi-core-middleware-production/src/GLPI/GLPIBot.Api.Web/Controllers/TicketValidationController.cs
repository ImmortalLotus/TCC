using GLPIBot.GLPI.Domain.Models;
using GLPIBot.GLPI.Domain.Models.Dto;
using GLPIBot.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace GLPIBot.Api.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketValidationController : BaseController
    {
        private readonly GlpiContext _context;

        public TicketValidationController(GlpiContext context)
        {
            this._context = context;
        }

        [HttpGet("{ticketId}", Name = "GetValidation")]
        [SwaggerOperation("Busca as validações de um chamado no GLPI")]
        public async Task<IActionResult> Get(int ticketId)
        {
            try
            {
                List<TicketValidationDto> tickets = await _context.TicketValidations.Where(x => x.TicketsId == ticketId)
                    .Select(x => new TicketValidationDto { Status = x.Status, TicketId = x.TicketsId, UsuarioValidador = x.UsersIdValidate }).ToListAsync();
                return Json(tickets);
            }
            catch (Exception ex)
            {   
                return Fail($"Ocorreu um erro ao buscar a validação do ticket soliticado: {ex.Message}");
            }
        }
    }
}
