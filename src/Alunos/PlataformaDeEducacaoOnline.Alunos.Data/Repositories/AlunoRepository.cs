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
    public class AlunoRepository(AlunosContext dbContext) : IAlunoRepository
    {
        private readonly DbSet<Aluno> _dbSet = dbContext.Set<Aluno>();
        public IUnitOfWork UnitOfWork => dbContext;

        public async Task<Aluno?> ObterPorId(Guid alunoId)
        {
            return await _dbSet.FindAsync(alunoId);
        }
        public async Task<Matricula?> ObterMatriculaPorCursoEAlunoId(Guid cursoId, Guid alunoId)
        {
            return await dbContext.Set<Matricula>()
                .AsNoTracking()
                .Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.CursoId == cursoId);
        }

        public async Task<IEnumerable<Matricula>> ObterMatriculasPendentePagamento(Guid alunoId)
        {
            return await dbContext.Set<Matricula>()
            .AsNoTracking()
                .Include(m => m.Status)
                .Where(m => m.AlunoId == alunoId && m.Status.Codigo == (int)EnumMatricula.AguardandoPagamento)
                .ToListAsync();
        }

        public async Task<Certificado?> ObterCertificadoPorId(Guid certificadoId, Guid alunoId)
        {
            return await dbContext.Set<Certificado>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == certificadoId && c.AlunoId == alunoId);
        }

        public void Adicionar(Aluno aluno)
        {
            _dbSet.Add(aluno);
        }

        public void AdicionarCertificado(Certificado certificado)
        {
            dbContext.Set<Certificado>().Add(certificado);
        }

        public void AdicionarMatricula(Matricula matricula)
        {
            dbContext.Set<Matricula>().Add(matricula);
        }
        public void AtualizarMatricula(Matricula matricula)
        {
            dbContext.Set<Matricula>().Update(matricula);
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }

    }
}
