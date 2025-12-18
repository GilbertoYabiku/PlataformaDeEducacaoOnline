using Microsoft.EntityFrameworkCore;
using PlataformaDeEducacaoOnline.Conteudos.Data.Context;
using PlataformaDeEducacaoOnline.Conteudos.Data.Repositories.Interfaces;
using PlataformaDeEducacaoOnline.Conteudos.Domain.Entities;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Conteudos.Data.Repositories;

public class AulaRepository(ConteudosContext dbContext) : IAulaRepository
{
    public IUnitOfWork UnitOfWork => dbContext;

    public void AdicionarProgressoAula(ProgressoAula progressoAula)
    {
        dbContext.Set<ProgressoAula>().Add(progressoAula);
    }
    public void AtualizarProgressoAula(ProgressoAula progressoAula)
    {
        dbContext.Set<ProgressoAula>().Update(progressoAula);
    }
    public async Task<ProgressoAula?> ObterProgressoAula(Guid aulaId, Guid alunoId)
    {
        return await dbContext.Set<ProgressoAula>()
            .FirstOrDefaultAsync(p => p.AulaId == aulaId && p.AlunoId == alunoId);
    }

    public void Dispose()
    {
        dbContext?.Dispose();
    }
}