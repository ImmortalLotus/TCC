namespace OrcHestrador.Domain.Models
{
    public class TicketRequest
    {
        public int TicketId { get; set; }
        public int IdCategoria { get; set; }
        public string Json { get; set; }
        public int Status { get; set; }

        public TicketRequest(int idCategoria, string json, int status, int ticketId)
        {
            IdCategoria = idCategoria;
            Json = json;
            Status = status;
            TicketId = ticketId;
        }
    }
}