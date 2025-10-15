using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Alunos.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> BuscarTodos();
        Task<TEntity> BuscarPorId(Guid id);
        Task<int> Adicionar(TEntity entity);
        Task<int> Atualizar(TEntity entity);
        Task<int> Excluir(Guid id);
        Task<int> Salvar();
    }
}
