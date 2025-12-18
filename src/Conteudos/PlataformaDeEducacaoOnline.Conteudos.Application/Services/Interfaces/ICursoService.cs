using PlataformaDeEducacaoOnline.Conteudos.Application.ViewModels;

namespace PlataformaDeEducacaoOnline.Conteudos.Application.Services.Interfaces;

public interface ICursoService
{
    Task<CursoViewModel?> ObterPorId(Guid cursoId);
    Task<IEnumerable<CursoViewModel>> ObterTodos();
    Task<HistoricoAprendizagemCursoViewModel?> ObterHistoricoAprendizagem(Guid cursoId, Guid usuarioId);
}