using OrcHestrador.Domain.Models.Dto;
using Refit;

namespace OrcHestrador.ApiClient
{
    public interface IGLPIClient
    {
        [Get("/TicketsPorGrupo/{groupId}")]
        Task<List<TicketIdDto>> buscarTicketsPorGrupo(int groupId);

        [Post("/FollowUp")]
        Task<ApiResponse<PostResponseDto>> addFollowUp(FollowUpInputDto followUp);

        [Post("/Solucao")]
        Task<ApiResponse<PostResponseDto>> addSolucao(SolucaoInputDto solucao);

        [Get("/FollowUp?TicketId={ticketId}")]
        Task<List<FollowUpDto>> buscarFollowUpPorTicket(int ticketId);

        [Post("/DesatribuirChamado?TicketId={ticketId}&GroupId={groupId}")]
        Task<PostResponseDto> desatribuirChamado(int ticketId, int groupId);

        [Post("/AtribuirChamado")]
        Task<ApiResponse<PostResponseDto>> atribuirChamado(int ticketId);
        [Get("/TicketValidation/{ticketId}")]
        Task<List<TicketValidationDto>> buscarValidacoesTicket(int ticketId);

        [Get("/BuscarUsuarioCpf?ticketId={ticketId}")]
        Task<UserDto> buscarUsuarioCpf(int ticketId);

        [Get("/Ticket/{ticketId}")]
        Task<TicketDto> buscarTicket(int ticketId);
    }
}