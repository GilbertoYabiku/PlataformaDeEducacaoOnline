using PlataformaDeEducacaoOnline.Financeiro.Application.Models;

namespace PlataformaDeEducacaoOnline.Financeiro.AntiCorruption.CartaoConnector.Interfaces;

public interface ICartaoGateway
{
    string GetPayPalServiceKey(string apiKey, string encriptionKey);
    string GetCardHashKey(string serviceKey, string cartaoCredito);
    Transacao CommitTransaction(string cardHashKey, string orderId, decimal amount);
}