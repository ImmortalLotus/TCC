namespace OrcHestrador.Domain.Models
{
    public class Automacao
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string NomeAutomacao { get; set; }
        public string SenhaAutomacao { get; set; }
        public IEnumerable<Rota> rotas;
    }
}