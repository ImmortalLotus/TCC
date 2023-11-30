using OrcHestrador.Domain.Models.Enums;

namespace OrcHestrador.Domain.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<Rota> rotas { get; set; }
        public TipoValidacao tipoValidacao { get; set; }
    }
}