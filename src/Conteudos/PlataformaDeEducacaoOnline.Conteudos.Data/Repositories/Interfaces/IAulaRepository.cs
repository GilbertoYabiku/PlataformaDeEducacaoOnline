using PlataformaDeEducacaoOnline.Conteudos.Domain.Entities;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Conteudos.Data.Repositories.Interfaces;

public interface IAulaRepository : IRepository<Aula>
{
    Task<ProgressoAula?> ObterProgressoAula(Guid aulaId, Guid alunoId);
    void AdicionarProgressoAula(ProgressoAula progressoAula);
    void AtualizarProgressoAula(ProgressoAula progressoAula);
}