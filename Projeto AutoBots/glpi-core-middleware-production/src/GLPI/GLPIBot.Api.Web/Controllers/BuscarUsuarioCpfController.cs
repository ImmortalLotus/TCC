using GLPIBot.GLPI.Domain.Models;
using GLPIBot.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace GLPIBot.Api.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuscarUsuarioCpfController : BaseController
    {

        private readonly GlpiContext _context;

        public BuscarUsuarioCpfController( GlpiContext context)
        {
            this._context = context;
        }

        [HttpGet(Name = "Buscar usuário")]
        [SwaggerOperation(Description = "Busca o usuário requerente com base no Id do Ticket")]
        public async Task<IActionResult> buscarUsuarioCpf(int ticketId)
        {
            try
            {
                var users = await _context.TicketUsers.Where(x => x.TicketId == ticketId).ToListAsync();
                if (users.Count == 0)
                {
                    return Fail("Não foi encontrado um usuário requerente neste chamado.");
                }
                var userId = users[0].UsersId;
                var user = _context.Users.Where(x=>x.Id==userId).First();
                return Json(user);

            }
            catch (Exception ex)
            {
                return Fail($"Ocorreu um erro ao buscar o ticket soliticado: {ex.Message}");
            }
        }
    }
}
