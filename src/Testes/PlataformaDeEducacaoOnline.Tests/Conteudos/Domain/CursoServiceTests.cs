using Moq;
using Moq.AutoMock;
using PlataformaDeEducacaoOnline.Conteudos.Application.Services;
using PlataformaDeEducacaoOnline.Conteudos.Data.Repositories.Interfaces;
using PlataformaDeEducacaoOnline.Conteudos.Domain.Entities;

namespace PlataformaDeEducacaoOnline.Tests.Conteudos.Domain;

public class CursoServiceTests
{
    private readonly AutoMocker _mocker;
    private readonly CursoService _service;
    private readonly Curso _curso;

    public CursoServiceTests()
    {
        _mocker = new AutoMocker();
        _service = _mocker.CreateInstance<CursoService>();
        _curso = new Curso("Curso Teste", "Descricao Teste", Guid.NewGuid(), 10);
    }

    [Fact(DisplayName = "Obter Curso Por Id")]
    [Trait("Categoria", "GestaoConteudos - CursoService")]
    public async Task ObterCursoPorId_CursoEncontrado_DeveRetornarComSucesso()
    {
        // Arrange
        var aula = new Aula("Aula 1", "Conteúdo 1");
        _curso.AdicionarAula(aula);
        _mocker.GetMock<ICursoRepository>().Setup(q => q.ObterPorId(_curso.Id))
            .ReturnsAsync(_curso);

        // Act
        var result = await _service.ObterPorId(_curso.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_curso.Id, result.Id);
        Assert.Equal(_curso.Nome, result.Nome);
        Assert.Equal(_curso.ConteudoProgramatico, result.ConteudoProgramatico);
        Assert.Equal(_curso.Preco, result.Preco);
        Assert.Single(result.Aulas);
        Assert.Equal(aula.Id, result.Aulas.First().Id);
    }
}