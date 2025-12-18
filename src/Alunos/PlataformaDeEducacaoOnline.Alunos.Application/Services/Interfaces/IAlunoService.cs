using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaDeEducacaoOnline.Alunos.Application.ViewModels;

namespace PlataformaDeEducacaoOnline.Alunos.Application.Services.Interfaces
{
    public interface IAlunoService
    {
        Task<MatriculaViewModel?> ObterMatricula(Guid cursoId, Guid alunoId);
        Task<IEnumerable<MatriculaViewModel>> ObterMatriculasPendentePagamento(Guid alunoId);
        Task<CertificadoViewModel> ObterCertificado(Guid certificadoId, Guid alunoId);
    }
}
