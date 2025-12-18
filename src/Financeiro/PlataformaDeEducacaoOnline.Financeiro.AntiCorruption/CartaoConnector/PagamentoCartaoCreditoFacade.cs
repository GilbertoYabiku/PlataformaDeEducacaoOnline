using Microsoft.Extensions.Options;
using PlataformaDeEducacaoOnline.Financeiro.AntiCorruption.CartaoConnector.Interfaces;
using PlataformaDeEducacaoOnline.Financeiro.AntiCorruption.Configuration;
using PlataformaDeEducacaoOnline.Financeiro.Application.Models;
using PlataformaDeEducacaoOnline.Financeiro.Application.Services.Interfaces;

namespace PlataformaDeEducacaoOnline.Financeiro.AntiCorruption.CartaoConnector;

public class PagamentoCartaoCreditoFacade(ICartaoGateway payPalGateway,
    IOptions<PagamentoSettings> options) : IPagamentoCartaoCreditoFacade
{
    private readonly PagamentoSettings _settings = options.Value;
    public Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento)
    {
        var apiKey = _settings.ApiKey;
        var encriptionKey = _settings.EncriptionKey;

        var serviceKey = payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
        var cardHashKey = payPalGateway.GetCardHashKey(serviceKey, pagamento.NumeroCartao);

        var transacao = payPalGateway.CommitTransaction(cardHashKey, pedido.CursoId.ToString(), pagamento.Valor);

        transacao.PagamentoId = pagamento.Id;

        return transacao;
    }
}