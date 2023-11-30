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
    public class TicketsPorGrupoController : BaseController
    {
        private readonly GlpiContext _context;

        public TicketsPorGrupoController(GlpiContext context)
        {
            this._context = context;
        }

        [HttpGet("{groupId}", Name = "GetTicketsByGroup")]
        [SwaggerOperation("Busca todos os chamados atribuídos a um grupo")]
        public async Task<IActionResult> Get(int groupId)
        {
            try
            {   
                //type 2 = atribuido tecnico
                List<TicketIdDto> tickets = await _context.GroupsTickets.Where(x => x.Groups_id == groupId && x.Type==2).Select(x => new TicketIdDto { TicketId = x.Tickets_id }).ToListAsync();
                return Json(tickets);
            }catch (Exception ex)
            {
                return Fail($"Ocorreu um erro ao buscar os tickets do grupo soliticado: {ex.Message}");
            }
        }
    }
}
