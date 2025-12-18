using Microsoft.EntityFrameworkCore;
using PlataformaDeEducacaoOnline.Core.Entities;
using PlataformaDeEducacaoOnline.Financeiro.Application.Models;
using PlataformaDeEducacaoOnline.Financeiro.Application.Services.Interfaces;
using PlataformaDeEducacaoOnline.Financeiro.Data.Context;

namespace PlataformaDeEducacaoOnline.Financeiro.Data.Repository;

public class PagamentoRepository(FinanceiroContext context) : IPagamentoRepository
{
    private readonly DbSet<Pagamento> _dbSet = context.Set<Pagamento>();
    public IUnitOfWork UnitOfWork => context;
    public void Adicionar(Pagamento pagamento)
    {
        _dbSet.Add(pagamento);
    }

    public void AdicionarTransacao(Transacao transacao)
    {
        context.Set<Transacao>().Add(transacao);
    }

    public void Dispose()
    {
       context.Dispose();
    }
}