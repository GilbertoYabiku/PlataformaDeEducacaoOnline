using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaDeEducacaoOnline.Alunos.Domain.Entities;

namespace PlataformaDeEducacaoOnline.Alunos.Data.Repositories.Interfaces
{
    public interface IStatusMatriculaRepository : IRepository<StatusMatricula>
    {
        Task<StatusMatricula?> ObterPorCodigo(int codigo);
    }
}
