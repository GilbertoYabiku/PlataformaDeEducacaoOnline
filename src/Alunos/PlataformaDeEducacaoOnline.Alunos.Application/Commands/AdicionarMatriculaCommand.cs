using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using PlataformaDeEducacaoOnline.Core.Bus;

namespace PlataformaDeEducacaoOnline.Alunos.Application.Commands
{
    public class AdicionarMatriculaCommand : Command
    {
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }

        public AdicionarMatriculaCommand(Guid alunoId, Guid cursoId)
        {
            AggregateId = alunoId;
            AlunoId = alunoId;
            CursoId = cursoId;
        }

        public override bool CommandValido()
        {
            ValidationResult = new AdicionarMatriculaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class AdicionarMatriculaCommandValidation : AbstractValidator<AdicionarMatriculaCommand>
    {
        public static string AlunoIdErro = "O campo AlunoId é obrigatório";
        public static string CursoIdErro = "O campo CursoId é obrigatório";
        public AdicionarMatriculaCommandValidation()
        {
            RuleFor(c => c.AlunoId)
                .NotEqual(Guid.Empty)
                .WithMessage(AlunoIdErro);
            RuleFor(c => c.CursoId)
                .NotEqual(Guid.Empty)
                .WithMessage(CursoIdErro);
        }
    }
}
