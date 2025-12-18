using PlataformaDeEducacaoOnline.Core.Entities;
using PlataformaDeEducacaoOnline.Financeiro.Application.Models;

namespace PlataformaDeEducacaoOnline.Financeiro.Application.Services.Interfaces;

public interface IPagamentoRepository : IRepository<Pagamento>
{
    void Adicionar(Pagamento pagamento);
    void AdicionarTransacao(Transacao transacao);
}