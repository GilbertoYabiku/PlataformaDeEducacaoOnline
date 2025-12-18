namespace PlataformaDeEducacaoOnline.Financeiro.Application.Models;

public class Pedido
{
    public Guid CursoId { get; set; }
    public Guid AlunoId { get; set; }
    public decimal Valor { get; set; }
}