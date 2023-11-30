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
    public class SolucaoController : BaseController
    {
        private readonly GlpiContext _context;
        private readonly GlpiOptions _glpiOptions;

        public SolucaoController(GlpiContext context, IOptions<GlpiOptions> options)
        {
            this._context = context;
            this._glpiOptions = options.Value;
        }

        [HttpPost(Name = "Add Solution")]
        [SwaggerOperation(Description ="Adiciona uma solução ao GLPI com base nos campos do schema")]
        public async Task<IActionResult> Post([FromBody] SolutionInput.InputS input)
        {
            input.SolutionType = "2";
            input.ItemType = "Ticket";
            try
            {
                Solution solution = new();
                solution.ItemsId = ConversorHelper.converterParaInteiro( input.TicketId);
                solution.Content=input.Content;
                solution.UsersId = ConversorHelper.converterParaInteiro(_glpiOptions.UserId);
                await _context.Solutions.AddAsync(solution);
                await _context.SaveChangesAsync();

                var ticket = _context.Tickets.Find(ConversorHelper.converterParaInteiro(input.TicketId));
                ticket.SolveDate = DateTime.UtcNow.AddHours(-4);
                ticket.Status = 5;
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();

                Result result = new();
                result.Id = solution.Id;
                result.Message = "Solução Adicionada com sucesso";
                return Json(result);
            }
            catch (Exception ex)
            {

                return Fail($"Ocorreu um erro ao adicionar a solução ao chamado, ou o chamado ja foi solucionado: {ex.Message}");
            }
        }
    }
}
