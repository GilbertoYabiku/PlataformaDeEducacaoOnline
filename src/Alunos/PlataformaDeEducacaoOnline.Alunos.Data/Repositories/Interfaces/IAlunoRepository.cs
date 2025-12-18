using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaDeEducacaoOnline.Alunos.Domain.Entities;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Alunos.Data.Repositories.Interfaces
{
    public interface IAlunoRepository : IRepository<Aluno>, IAggregateRoot
    {
        Task<Aluno?> ObterPorId(Guid id);
        Task<Matricula?> ObterMatriculaPorCursoEAlunoId(Guid cursoId, Guid alunoId);
        Task<IEnumerable<Matricula>> ObterMatriculasPendentePagamento(Guid alunoId);
        Task<Certificado?> ObterCertificadoPorId(Guid certificadoId, Guid alunoId);

        void AdicionarMatricula(Matricula matricula);
        void AtualizarMatricula(Matricula matricula);
        void Adicionar(Aluno aluno);
        void AdicionarCertificado(Certificado certificado);
    }
}
