namespace OrcHestrador.Domain.Models
{
    public class Mensagem
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string UrlOrigem { get; set; }
        public bool Status { get; set; }
        public int TicketId { get; set; }

        public Mensagem(string message, string urlOrigem, bool status, int ticketId)
        {
            Message = message;
            UrlOrigem = urlOrigem;
            Status = status;
            this.TicketId = ticketId;
        }
    }
}