using MediatR;
using Moq;
using Moq.AutoMock;
using PlataformaDeEducacaoOnline.Alunos.Application.CommandHandlers;
using PlataformaDeEducacaoOnline.Alunos.Application.Commands;
using PlataformaDeEducacaoOnline.Alunos.Data.Repositories.Interfaces;
using PlataformaDeEducacaoOnline.Alunos.Domain.Entities;
using PlataformaDeEducacaoOnline.Core.Bus.Notifications;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Tests.Alunos.Domain;

public class MatriculaCommandHandlerTests
{
    private readonly AutoMocker _mocker;
    private readonly MatriculaCommandHandler _handler;
    private readonly Mock<IAlunoRepository> _alunoRepositoryMock;
    private readonly Mock<IStatusMatriculaRepository> _statusMatriculaRepositoryMock;
    private readonly Aluno _aluno;
    private readonly Guid _cursoId;
    private readonly Guid _alunoId;

    public MatriculaCommandHandlerTests()
    {
        _mocker = new AutoMocker();
        _handler = _mocker.CreateInstance<MatriculaCommandHandler>();
        _alunoRepositoryMock = _mocker.GetMock<IAlunoRepository>();
        _statusMatriculaRepositoryMock = _mocker.GetMock<IStatusMatriculaRepository>();
        _aluno = new Aluno(Guid.NewGuid(), "teste");
        _cursoId = Guid.NewGuid();
        _alunoId = Guid.NewGuid(); 
    }
    [Fact(DisplayName = "Criar Matricula Com Sucesso")]
    [Trait("Categoria", "GestaoAlunos - MatriculaCommandHandler")]
    public async Task AdicionarMatricula_NovaMatricula_DeveExecutarComSucesso()
    {
        // Arrange
        var command = new AdicionarMatriculaCommand(_alunoId, _cursoId);

        _alunoRepositoryMock.Setup(r => r.ObterPorId(_alunoId)).ReturnsAsync(_aluno);
        _alunoRepositoryMock.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
        _statusMatriculaRepositoryMock.Setup(s => s.ObterPorCodigo((int)EnumMatricula.Iniciada))
            .ReturnsAsync(new StatusMatricula { Codigo = (int)EnumMatricula.Iniciada });
        _statusMatriculaRepositoryMock.Setup(s => s.ObterPorCodigo((int)EnumMatricula.AguardandoPagamento))
            .ReturnsAsync(new StatusMatricula { Codigo = (int)EnumMatricula.AguardandoPagamento });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        _alunoRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        _alunoRepositoryMock.Verify(r => r.ObterPorId(_alunoId), Times.Once);
        _statusMatriculaRepositoryMock.Verify(s => s.ObterPorCodigo((int)EnumMatricula.Iniciada), Times.Once);
        _statusMatriculaRepositoryMock.Verify(s => s.ObterPorCodigo((int)EnumMatricula.AguardandoPagamento),
            Times.Once);
        _alunoRepositoryMock.Verify(r => r.AdicionarMatricula(It.IsAny<Matricula>()), Times.Once);
    }

    [Fact(DisplayName = "Criar Matricula Com Erro - Command")]
    [Trait("Categoria", "GestaoAlunos - MatriculaCommandHandler")]
    public async Task EhValido_CommandInvalido_NãoDeveExecutarComSucesso()
    {
        // Arrange
        var command = new AdicionarMatriculaCommand(Guid.Empty, Guid.Empty);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
        Assert.Equal(2, command.ValidationResult.Errors.Count);
        Assert.Contains(AdicionarMatriculaCommandValidation.CursoIdErro,
            command.ValidationResult.Errors.Select(e => e.ErrorMessage));
        Assert.Contains(AdicionarMatriculaCommandValidation.AlunoIdErro,
            command.ValidationResult.Errors.Select(e => e.ErrorMessage));
        _alunoRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        _alunoRepositoryMock.Verify(r => r.ObterPorId(_alunoId), Times.Never);
        _alunoRepositoryMock.Verify(r => r.AdicionarMatricula(It.IsAny<Matricula>()), Times.Never);
        _mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<DomainNotification>(), CancellationToken.None), Times.Exactly(2));
    }

    [Fact(DisplayName = "Criar Matricula Com Erro - Aluno Não Encontrado")]
    [Trait("Categoria", "GestaoAlunos - MatriculaCommandHandler")]
    public async Task AdicionarMatricula_AlunoNãoEncontrado_NãoDeveExecutarComSucesso()
    {
        // Arrange
        var command = new AdicionarMatriculaCommand(_alunoId, _cursoId);

        _mocker.GetMock<IAlunoRepository>().Setup(r => r.ObterPorId(command.AlunoId)).ReturnsAsync((Aluno?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
        _alunoRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        _alunoRepositoryMock.Verify(r => r.ObterPorId(command.AlunoId), Times.Once);
    }

    [Fact(DisplayName = "Criar Matricula Com Erro - Matricula Existente")]
    [Trait("Categoria", "GestaoAlunos - MatriculaCommandHandler")]
    public async Task AdicionarMatricula_MatriculaExistente_NãoDeveExecutarComSucesso()
    {
        // Arrange
        var command = new AdicionarMatriculaCommand(_alunoId, _cursoId);
        var statusIniciada = new StatusMatricula
        {
            Codigo = (int)EnumMatricula.Iniciada
        };
        var matriculaExistente = new Matricula(_alunoId, _cursoId, statusIniciada);

        _alunoRepositoryMock.Setup(r => r.ObterPorId(_alunoId)).ReturnsAsync(_aluno);
        _alunoRepositoryMock.Setup(r => r.ObterMatriculaPorCursoEAlunoId(_cursoId, _aluno.Id))
            .ReturnsAsync(matriculaExistente);
        _alunoRepositoryMock.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
        _alunoRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        _alunoRepositoryMock.Verify(r => r.ObterPorId(_alunoId), Times.Once);
        _statusMatriculaRepositoryMock.Verify(s => s.ObterPorCodigo((int)EnumMatricula.Iniciada), Times.Never);
        _alunoRepositoryMock.Verify(r => r.AdicionarMatricula(It.IsAny<Matricula>()), Times.Never);
    }

    [Fact(DisplayName = "Ativar Matricula Com Sucesso")]
    [Trait("Categoria", "GestaoAlunos - MatriculaCommandHandler")]
    public async Task Ativar_MatriculaStatusAguardandoPagamento_DeveExecutarComSucesso()
    {
        // Arrange
        var command = new AtivarMatriculaCommand(_alunoId, _cursoId);
        var statusAtiva = new StatusMatricula
        {
            Codigo = (int)EnumMatricula.Ativa
        };
        var statusIniciada = new StatusMatricula
        {
            Codigo = (int)EnumMatricula.Iniciada,
        };
        var matricula = new Matricula(_alunoId, _cursoId, statusIniciada);


        _alunoRepositoryMock.Setup(r => r.ObterMatriculaPorCursoEAlunoId(command.CursoId, command.AlunoId))
            .ReturnsAsync(matricula);
        _statusMatriculaRepositoryMock.Setup(s => s.ObterPorCodigo((int)EnumMatricula.Ativa))
            .ReturnsAsync(statusAtiva);
        _alunoRepositoryMock.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        _alunoRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        _statusMatriculaRepositoryMock.Verify(s => s.ObterPorCodigo((int)EnumMatricula.Ativa), Times.Once);
        _alunoRepositoryMock.Verify(r => r.AtualizarMatricula(It.IsAny<Matricula>()), Times.Once);
    }

    [Fact(DisplayName = "Ativar Matricula Falha")]
    [Trait("Categoria", "GestaoAlunos - MatriculaCommandHandler")]
    public async Task Ativar_MatriculaNaoEncontrada_NaoDeveAtualizar()
    {
        // Arrange
        var command = new AtivarMatriculaCommand(_alunoId, _cursoId);

        _alunoRepositoryMock.Setup(r => r.ObterMatriculaPorCursoEAlunoId(command.CursoId, command.AlunoId))
            .ReturnsAsync((Matricula?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
        _alunoRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        _alunoRepositoryMock.Verify(r => r.AtualizarMatricula(It.IsAny<Matricula>()), Times.Never);
        _mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<DomainNotification>(), CancellationToken.None), Times.Once);
    }
    [Fact(DisplayName = "Ativar Matricula Falha - ValidarCommand")]
    [Trait("Categoria", "GestaoAlunos - MatriculaCommandHandler")]
    public async Task ValidarCommand_MatriculaNaoEncontrada_NaoDeveAtualizar()
    {
        // Arrange
        var command = new AtivarMatriculaCommand(Guid.Empty, Guid.Empty);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
        Assert.Equal(2, command.ValidationResult.Errors.Count);
        Assert.Contains(AtivarMatriculaCommandValidation.AlunoIdErro,
            command.ValidationResult.Errors.Select(e => e.ErrorMessage));
        Assert.Contains(AtivarMatriculaCommandValidation.CursoIdErro,
            command.ValidationResult.Errors.Select(e => e.ErrorMessage));
        _alunoRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        _alunoRepositoryMock.Verify(r => r.AtualizarMatricula(It.IsAny<Matricula>()), Times.Never);
        _mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<DomainNotification>(), CancellationToken.None), Times.Exactly(2));
    }
}