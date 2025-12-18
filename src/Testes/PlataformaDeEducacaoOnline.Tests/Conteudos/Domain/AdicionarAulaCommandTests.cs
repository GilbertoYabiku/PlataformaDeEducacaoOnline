using PlataformaDeEducacaoOnline.Conteudos.Application.Commands;

namespace PlataformaDeEducacaoOnline.Tests.Conteudos.Domain;

public class AdicionarAulaCommandTests
{
    [Fact(DisplayName = "Adicionar Aula Command Válido")]
    [Trait("Categoria", "GestaoConteudos - AulaCommand")]
    public void EhValido_CommandValido_DeveEstarValido()
    {
        // Arrange
        var cursoId = Guid.NewGuid();
        var command = new AdicionarAulaCommand("Aula 1", "Conteudo da aula", cursoId, "nome material", "ZIP");

        // Act
        var result = command.CommandValido();

        // Assert
        Assert.True(result);
    }

    [Fact(DisplayName = "Adicionar Aula Command Inválido")]
    [Trait("Categoria", "GestaoConteudos - AulaCommand")]
    public void EhValido_CommandInvalido_DeveEstarInvalido()
    {
        // Arrange
        var command = new AdicionarAulaCommand("", "", Guid.Empty, "", "");

        // Act
        var result = command.CommandValido();

        // Assert
        Assert.False(result);
        Assert.Equal(3, command.ValidationResult.Errors.Count);
        Assert.Contains(AdicionarAulaCommandValidation.NomeErro, command.ValidationResult.Errors.Select(e => e.ErrorMessage));
        Assert.Contains(AdicionarAulaCommandValidation.ConteudoErro, command.ValidationResult.Errors.Select(e => e.ErrorMessage));
        Assert.Contains(AdicionarAulaCommandValidation.CursoIdErro, command.ValidationResult.Errors.Select(e => e.ErrorMessage));
    }
}