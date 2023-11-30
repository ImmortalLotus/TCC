using GLPIBot.GLPI.Domain.Models;
using GLPIBot.GLPI.Domain.Models.Dto;
using GLPIBot.Infra;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GLPIBot.Api.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : BaseController
    {
        private readonly GlpiContext _context;

        public TicketController(GlpiContext context)
        {
            this._context = context;
        }

        [HttpGet("{ticketId}", Name = "GetTicket")]
        [SwaggerOperation("Busca um chamado com base no Id dele")]
        public async Task<IActionResult> Get(int ticketId)
        {
            try
            {
                Ticket ticketNoBanco= await _context.Tickets.FindAsync(ticketId);
                TicketDto ticket = new()
                {
                    Category = ticketNoBanco.ITILCategoriesId,
                    Id=ticketNoBanco.Id,
                    Status=ticketNoBanco.Status,    
                    Name=ticketNoBanco.Name,
                    UsuarioCliente=ticketNoBanco.UsersIdRecipient,
                    Content=ticketNoBanco.Content,
                };
                return Json(ticket);
            }
            catch (Exception ex)
            {
                return Fail($"Ocorreu um erro ao buscar o ticket soliticado: {ex.Message}");
            }
        }
    }
}
