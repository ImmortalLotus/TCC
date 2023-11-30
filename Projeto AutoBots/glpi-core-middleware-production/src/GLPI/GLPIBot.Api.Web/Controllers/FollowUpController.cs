using GLPIBot.Api.Web.Helper;
using GLPIBot.GLPI.Domain.Models;
using GLPIBot.GLPI.Domain.Models.Dto;
using GLPIBot.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace GLPIBot.Api.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FollowUpController : BaseController
    {
        private readonly GlpiContext _context;
        private readonly GlpiOptions _glpiOptions;

        public FollowUpController(IOptions<GlpiOptions> glpiOptions,GlpiContext context)
        {
            this._context = context;
            this._glpiOptions = glpiOptions.Value;
        }


        [HttpPost(Name = "AddFollowUp")]
        [SwaggerOperation(Description = "Adiciona uma mensagem ao chamado conforme os parâmetros do schema")]
        public async Task<IActionResult> Post([FromBody] FollowUpInput.InputF input)
        {
            try
            {
                ITILFollowUp followup = new();
                followup.IsPrivate = ConversorHelper.converterParaInteiro(input.IsPrivate);
                followup.ItemsId = ConversorHelper.converterParaInteiro(input.TicketId);
                followup.Content = input.Content;
                followup.RequestTypesId = ConversorHelper.converterParaInteiro(input.RequestTypesId);
                followup.UsersId = ConversorHelper.converterParaInteiro(_glpiOptions.UserId);
                await _context.FollowUps.AddAsync(followup);

                await _context.SaveChangesAsync();

                Result result = new();
                result.Id = followup.Id;
                result.Message = "FollowUp adicionado com sucesso";
                return Json(result);
            }
            catch (Exception ex)
            {
                return Fail($"Ocorreu um erro ao adicionar a mensagem ao chamado: {ex.Message}");
            }
        }

        [HttpGet(Name = "GetFollowUp")]
        [SwaggerOperation(Description ="Busca todas as mensagens vinculadas a este chamado")]
        public async Task<IActionResult> Get(int TicketId)
        {
            try
            {
                List<FollowUpDto> followUps = await _context.FollowUps.Where(x => x.ItemType.Equals("Ticket") && x.ItemsId == TicketId).Select(x => new FollowUpDto {UsersId=x.UsersId,TicketId=x.ItemsId}).ToListAsync();
                return Json(followUps);
            }
            catch (Exception ex)
            {
                return Fail($"Ocorreu um erro ao buscar as mensagens do chamado: {ex.Message}");
            }
        }
    }
}
