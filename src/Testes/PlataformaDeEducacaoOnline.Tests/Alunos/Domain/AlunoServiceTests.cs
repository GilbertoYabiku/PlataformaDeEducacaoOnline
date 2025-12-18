using Moq;
using Moq.AutoMock;
using PlataformaDeEducacaoOnline.Alunos.Application.Services;
using PlataformaDeEducacaoOnline.Alunos.Data.Repositories.Interfaces;
using PlataformaDeEducacaoOnline.Alunos.Domain.Entities;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Tests.Alunos.Domain;

public class AlunoServiceTests
{
    private readonly AutoMocker _mocker;
    private readonly AlunoService _service;
    private readonly Guid _cursoId;
    private readonly Guid _alunoId;

    public AlunoServiceTests()
    {
        _mocker = new AutoMocker();
        _service = _mocker.CreateInstance<AlunoService>();
        _cursoId = Guid.NewGuid();
        _alunoId = Guid.NewGuid();
    }

    [Fact(DisplayName = "Obter Matricula")]
    [Trait("Categoria", "GestaoAlunos - AlunoService")]
    public async Task ObterMatricula_MatriculaEncontrada_DeveRetornarComSucesso()
    {
        // Arrange
        var statusIniciada = new StatusMatricula
        {
            Codigo = (int)EnumMatricula.Iniciada,
        };
        var matricula = new Matricula(_alunoId, _cursoId, statusIniciada);

        _mocker.GetMock<IAlunoRepository>().Setup(q => q.ObterMatriculaPorCursoEAlunoId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(matricula);

        // Act
        var result = await _service.ObterMatricula(_cursoId, _alunoId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_alunoId, result.AlunoId);
        Assert.Equal(_cursoId, result.CursoId);
    }

    [Fact(DisplayName = "Obter Matriculas Pendente Pagamento")]
    [Trait("Categoria", "GestaoAlunos - AlunoService")]
    public async Task ObterMatriculasPendentePagamento_MatriculasEncontradas_DeveRetornarComSucesso()
    {
        // Arrange
        var statusAguardandoPag = new StatusMatricula
        {
            Codigo = (int)EnumMatricula.AguardandoPagamento,
        };
        var statusIniciada = new StatusMatricula
        {
            Codigo = (int)EnumMatricula.Iniciada,
        };
        var matriculas = new List<Matricula>()
        {
            new(_alunoId, _cursoId, statusIniciada)
        };
        matriculas[0].AguardandoPagamento(statusAguardandoPag);

        _mocker.GetMock<IAlunoRepository>().Setup(q => q.ObterMatriculasPendentePagamento(It.IsAny<Guid>()))
            .ReturnsAsync(matriculas);

        // Act
        var result = await _service.ObterMatriculasPendentePagamento(_alunoId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Count());
        Assert.Collection(result, (item) =>
        {
           Assert.Equal(item.AlunoId, _alunoId);
           Assert.Equal(item.CursoId, _cursoId);
           Assert.Equal((int)EnumMatricula.AguardandoPagamento, item.Status);
        });
    }
}