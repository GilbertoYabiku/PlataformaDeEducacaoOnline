using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaDeEducacaoOnline.Alunos.Domain.Entities;

namespace PlataformaDeEducacaoOnline.Alunos.Data.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        void Adicionar(Usuario usuario);
    }
}
