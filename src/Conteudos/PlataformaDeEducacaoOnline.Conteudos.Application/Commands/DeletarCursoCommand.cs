using FluentValidation;
using PlataformaDeEducacaoOnline.Core.Bus;

namespace PlataformaDeEducacaoOnline.Conteudos.Application.Commands;

public class DeletarCursoCommand : Command
{
    public Guid CursoId { get; set; }

    public DeletarCursoCommand(Guid cursoId)
    {
        AggregateId = cursoId;
        CursoId = cursoId;
    }
    public override bool CommandValido()
    {
        ValidationResult = new DeletarCursoCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
public class DeletarCursoCommandValidation : AbstractValidator<DeletarCursoCommand>
{
    public static string CursoIdErro => "O ID é obrigatório";
    public DeletarCursoCommandValidation()
    {
        RuleFor(c => c.CursoId).NotEmpty().WithMessage(CursoIdErro);
    }
}