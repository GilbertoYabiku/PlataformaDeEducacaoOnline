using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Financeiro.Application.Models;

public class Transacao : BaseEntity
{
    public Guid MatriculaId { get; set; }
    public Guid PagamentoId { get; set; }
    public decimal Total { get; set; }
    public StatusTransacao StatusTransacao { get; set; }

    public Pagamento Pagamento { get; set; }
}