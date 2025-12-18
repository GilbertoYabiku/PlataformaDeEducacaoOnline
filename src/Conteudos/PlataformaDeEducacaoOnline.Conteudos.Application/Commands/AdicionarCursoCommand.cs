using FluentValidation;
using PlataformaDeEducacaoOnline.Core.Bus;

namespace PlataformaDeEducacaoOnline.Conteudos.Application.Commands;

public class AdicionarCursoCommand : Command
{
    public string Nome { get; set; }
    public string ConteudoProgramatico { get; set; }
    public Guid UsuarioCriacaoId { get; set; }
    public decimal Preco { get; set; }

    public AdicionarCursoCommand(string nome, string conteudoProgramatico, Guid usuarioCriacaoId, decimal preco)
    {
        AggregateId = usuarioCriacaoId;
        Nome = nome;
        ConteudoProgramatico = conteudoProgramatico;
        UsuarioCriacaoId = usuarioCriacaoId;
        Preco = preco;
    }

    public override bool CommandValido()
    {
        ValidationResult = new AdicionarCursoCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class AdicionarCursoCommandValidation : AbstractValidator<AdicionarCursoCommand>
{
    public static string NomeErro => "O Nome é obrigatório";
    public static string ConteudoProgramaticoErro => "O Conteúdo Programático é obrigatório";
    public static string UsuarioCriacaoIdErro => "O ID do Usuário é obrigatório";
    public static string PrecoErro => "O Preço do curso deve ser maior que zero";

    public AdicionarCursoCommandValidation()
    {
        RuleFor(c => c.Nome).NotEmpty().WithMessage(NomeErro);

        RuleFor(c => c.ConteudoProgramatico).NotEmpty().WithMessage(ConteudoProgramaticoErro);

        RuleFor(c => c.UsuarioCriacaoId).NotEmpty().WithMessage(UsuarioCriacaoIdErro);

        RuleFor(c => c.Preco)
            .GreaterThan(0)
            .WithMessage(PrecoErro);
    }
}