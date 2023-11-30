namespace OrcHestrador.Domain.Models
{
    public class CampoRota
    {
        public Guid Id { get; set; }
        public string CampoApi { get; set; }
        public Rota Rota { get; set; }
        public Guid IdRota { get; set; }
    }
}