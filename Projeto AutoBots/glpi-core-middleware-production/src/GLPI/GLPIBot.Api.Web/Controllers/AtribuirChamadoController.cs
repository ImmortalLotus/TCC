using GLPIBot.Api.Web.Helper;
using GLPIBot.GLPI.Domain.Models;
using GLPIBot.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace GLPIBot.Api.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtribuirChamadoController : BaseController
    {
        private readonly GlpiContext _context;
        private readonly GlpiOptions _glpiOptions;

        public AtribuirChamadoController(IOptions<GlpiOptions> options, GlpiContext context)
        {

            this._context = context;
            _glpiOptions = options.Value;
        }

        [HttpPost(Name = "AtribuirChamado")]
        [SwaggerOperation(Description = "Atribui o usuário Ada L. ao chamado")]
        public async Task<IActionResult> Post(int ticketId)
        {
            try
            {
                TicketUsers ticketUser = new();
                ticketUser.TicketId = ticketId;
                ticketUser.UsersId = ConversorHelper.converterParaInteiro(_glpiOptions.UserId);
                ticketUser.Type = 2;
                await _context.TicketUsers.AddAsync(ticketUser);
                await _context.SaveChangesAsync();

                Result result = new();
                result.Id = ticketUser.Id;
                result.Message = "Usuário atribuído com sucesso";
                return Json(result);
            }
            catch (Exception ex)
            {

                return Fail($"Ocorreu um erro ao adicionar a mensagem ao chamado: {ex.Message}");
            }
        }
    }
}
