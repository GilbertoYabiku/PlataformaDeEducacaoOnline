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
    public class UsuarioRepository(AlunosContext dbContext) : IUsuarioRepository
    {
        private readonly DbSet<Usuario> _dbSet = dbContext.Set<Usuario>();
        public IUnitOfWork UnitOfWork => dbContext;
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
