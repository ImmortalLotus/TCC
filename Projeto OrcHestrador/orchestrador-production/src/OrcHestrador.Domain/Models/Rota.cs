namespace OrcHestrador.Domain.Models
{
    public class Rota
    {
        public Guid Id { get; set; }
        public String RotaApi { get; set; }
        public Guid IdAutomacao { get; set; }
        public int IdCategoria { get; set; }
        public Automacao Automacao { get; set; }
        public Categoria Categoria { get; set; }
        public IEnumerable<CampoRota> camposRota { get; set; }
    }
}