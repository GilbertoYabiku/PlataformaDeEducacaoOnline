using PlataformaDeEducacaoOnline.Financeiro.Application.Models;

namespace PlataformaDeEducacaoOnline.Financeiro.Application.Services.Interfaces;

public interface IPagamentoCartaoCreditoFacade
{
    Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento);
}