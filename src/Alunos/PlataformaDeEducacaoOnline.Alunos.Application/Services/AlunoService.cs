using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaDeEducacaoOnline.Alunos.Application.Services.Interfaces;
using PlataformaDeEducacaoOnline.Alunos.Application.ViewModels;
using PlataformaDeEducacaoOnline.Alunos.Data.Repositories.Interfaces;

namespace PlataformaDeEducacaoOnline.Alunos.Application.Services
{
    public class AlunoService(IAlunoRepository alunoRepository) : IAlunoService
    {
        public async Task<MatriculaViewModel?> ObterMatricula(Guid cursoId, Guid alunoId)
        {
            var matricula = await alunoRepository.ObterMatriculaPorCursoEAlunoId(cursoId, alunoId);

            if (matricula is null)
                return null;

            return new MatriculaViewModel
            {
                Id = matricula.Id,
                AlunoId = matricula.AlunoId,
                CursoId = matricula.CursoId,
                Status = matricula.Status.Codigo,
                StatusDescricao = matricula.Status.Descricao,
                DataMatricula = matricula.DataMatricula
            };
        }

        public async Task<IEnumerable<MatriculaViewModel>> ObterMatriculasPendentePagamento(Guid alunoId)
        {
            var matriculas = await alunoRepository.ObterMatriculasPendentePagamento(alunoId);

            return matriculas.Select(m => new MatriculaViewModel
            {
                Id = m.Id,
                AlunoId = m.AlunoId,
                CursoId = m.CursoId,
                Status = m.Status.Codigo,
                StatusDescricao = m.Status.Descricao,
                DataMatricula = m.DataMatricula
            }).ToList();
        }

        public async Task<CertificadoViewModel> ObterCertificado(Guid certificadoId, Guid alunoId)
        {
            var certificado = await alunoRepository.ObterCertificadoPorId(certificadoId, alunoId);

            return new CertificadoViewModel
            {
                Arquivo = certificado?.Arquivo ?? null
            };
        }
    }
}
