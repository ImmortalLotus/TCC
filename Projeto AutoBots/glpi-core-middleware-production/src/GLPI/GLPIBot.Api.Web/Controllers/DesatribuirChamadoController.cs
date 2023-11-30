using GLPIBot.GLPI.Domain.Models;
using GLPIBot.Infra;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GLPIBot.Api.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DesatribuirChamadoController : BaseController
    {
        private readonly GlpiContext _context;

        public DesatribuirChamadoController(GlpiContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "DesatribuirChamado")]
        [SwaggerOperation(Description = "Desatribui o chamado do grupo atual, e atribui ao outro grupo")]
        public async Task<IActionResult> Post(int TicketId, int GroupId)
        {
            try
            {
                List<Group_Ticket> groupTickets= _context.GroupsTickets.Where(x => x.Tickets_id == TicketId).ToList();
                groupTickets.ForEach(x=>x.Groups_id=GroupId);
                foreach (var groupTicket in groupTickets)
                {
                    _context.Update(groupTicket);
                }

                await _context.SaveChangesAsync();
                return Success("Desatribuido com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
