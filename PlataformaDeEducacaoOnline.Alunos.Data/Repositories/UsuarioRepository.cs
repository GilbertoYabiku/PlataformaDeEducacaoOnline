using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlataformaDeEducacaoOnline.Alunos.Data.Context;
using PlataformaDeEducacaoOnline.Alunos.Domain.Entities;

namespace PlataformaDeEducacaoOnline.Alunos.Data.Repositories
{
    public class UsuarioRepository(AlunosContext dbContext) 
    {
        private readonly DbSet<Usuario> _dbSet = dbContext.Set<Usuario>();
        public void Adicionar(Usuario usuario)
        {
            _dbSet.Add(usuario);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
