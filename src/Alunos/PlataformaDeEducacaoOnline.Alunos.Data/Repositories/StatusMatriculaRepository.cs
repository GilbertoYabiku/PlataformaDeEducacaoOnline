using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlataformaDeEducacaoOnline.Alunos.Data.Context;
using PlataformaDeEducacaoOnline.Alunos.Data.Repositories.Interfaces;
using PlataformaDeEducacaoOnline.Alunos.Domain.Entities;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Alunos.Data.Repositories
{
    public class StatusMatriculaRepository(AlunosContext dbContext) : IStatusMatriculaRepository
    {
        private readonly DbSet<StatusMatricula> _dbSet = dbContext.Set<StatusMatricula>();
        public IUnitOfWork UnitOfWork => dbContext;
        public async Task<StatusMatricula?> ObterPorCodigo(int codigo)
        {
            return await _dbSet
                .FirstOrDefaultAsync(s => s.Codigo == codigo);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
