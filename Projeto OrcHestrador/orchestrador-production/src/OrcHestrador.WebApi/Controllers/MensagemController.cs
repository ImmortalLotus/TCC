using Microsoft.AspNetCore.Mvc;
using OrcHestrador.Domain.Models;
using OrcHestrador.Domain.Models.Dto;
using OrcHestrador.Infra.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace OrcHestrador.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MensagemController : BaseController
    {
        private readonly ILogger<MensagemController> _logger;
        private readonly OrcHestradorContext _context;

        public MensagemController(ILogger<MensagemController> logger, OrcHestradorContext context)
        {
            _logger = logger;
            this._context = context;
        }

        [HttpPost(Name = "PostMensagem")]
        [SwaggerOperation(Description = "Envia uma mensagem para o OrcHestrador, que depois vai enviar ela para o GLPI")]
        public async Task<IActionResult> Post(MensagemDto mensagemDto)
        {
            try
            {
                _context.Add(new Mensagem(mensagemDto.Message, mensagemDto.UrlOrigem, mensagemDto.Status, mensagemDto.TicketId));
                await _context.SaveChangesAsync();
                return Json(mensagemDto.Message);
            }
            catch (Exception ex)
            {
                return Fail($"Ocorreu um erro ao enviar a mensagem {ex.Message}");
            }
        }
    }
}