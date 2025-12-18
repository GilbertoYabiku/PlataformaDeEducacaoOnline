using PlataformaDeEducacaoOnline.Conteudos.Application.Services.Interfaces;
using PlataformaDeEducacaoOnline.Conteudos.Application.ViewModels;
using PlataformaDeEducacaoOnline.Conteudos.Data.Extensions;
using PlataformaDeEducacaoOnline.Conteudos.Data.Repositories.Interfaces;

namespace PlataformaDeEducacaoOnline.Conteudos.Application.Services;

public class CursoService(ICursoRepository cursoRepository) : ICursoService
{
    public async Task<CursoViewModel?> ObterPorId(Guid cursoId)
    {
        var curso = await cursoRepository.ObterPorId(cursoId);

        if (curso is null)
            return null;

        return new CursoViewModel
        {
            Id = curso.Id,
            Nome = curso.Nome,
            ConteudoProgramatico = curso.ConteudoProgramatico,
            Preco = curso.Preco,
            Aulas = curso.Aulas?.Select(a => new AulaViewModel
            {
                Id = a.Id,
                Nome = a.Nome,
                Conteudo = a.Conteudo
            }).ToList() ?? []
        };
    }

    public async Task<IEnumerable<CursoViewModel>> ObterTodos()
    {
        var cursos = await cursoRepository.ObterTodos(); 

        return cursos.Select(c => new CursoViewModel
        {
            Id = c.Id,
            Nome = c.Nome,
            ConteudoProgramatico = c.ConteudoProgramatico,
            Preco = c.Preco,
            Aulas = c.Aulas?.Select(a => new AulaViewModel
            {
                Id = a.Id,
                Nome = a.Nome,
                Conteudo = a.Conteudo
            }).ToList() ?? []
        }).ToList();
    }

    public async Task<HistoricoAprendizagemCursoViewModel?> ObterHistoricoAprendizagem(Guid cursoId, Guid usuarioId)
    {
        var progressoCurso = await cursoRepository.ObterProgressoCurso(cursoId, usuarioId);

        if (progressoCurso is null)
            return null;

        var progressosAulas = await cursoRepository.ObterProgressoAulas(cursoId, usuarioId);

        return new HistoricoAprendizagemCursoViewModel
        {
            CursoId = progressoCurso.CursoId,
            NomeCurso = progressoCurso.Curso.Nome,
            PercentualConcluido = progressoCurso.PercentualConcluido,
            AulasConcluidas = progressoCurso.AulasConcluidas,
            TotalAulas = progressoCurso.TotalAulas,
            Aulas = progressosAulas.Select(a => new HistoricoAprendizagemAulaViewModel
            {
                AulaId = a.AulaId,
                NomeAula = a.Aula.Nome,
                Status = a.Status.GetDescription()
            }).ToList()
        };
    }
}