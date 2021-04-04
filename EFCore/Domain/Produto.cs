using EFCore.ValueObjects;

namespace EFCore.Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string CodigoBarra { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public TipoProduto TipoProduto { get; set; }
        public bool Ativo { get; set; }
    }
}