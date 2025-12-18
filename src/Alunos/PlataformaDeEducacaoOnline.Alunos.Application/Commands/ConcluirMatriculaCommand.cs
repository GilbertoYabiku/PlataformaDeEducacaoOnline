using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using PlataformaDeEducacaoOnline.Core.Bus;

namespace PlataformaDeEducacaoOnline.Alunos.Application.Commands
{
    public class ConcluirMatriculaCommand : Command
    {
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public string NomeCurso { get; set; }

        public ConcluirMatriculaCommand(Guid alunoId, Guid cursoId, string nomeCurso)
        {
            AggregateId = alunoId;
            AlunoId = alunoId;
            CursoId = cursoId;
            NomeCurso = nomeCurso;
        }
        public override bool CommandValido()
        {
            ValidationResult = new ConcluirMatriculaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class ConcluirMatriculaCommandValidation : AbstractValidator<ConcluirMatriculaCommand>
    {
        public static string AlunoId = "O campo AlunoId é obrigatório";
        public static string CursoId = "O campo CursoId é obrigatório";
        public static string NomeCurso = "O campo Nome Curso é obrigatório";
        public ConcluirMatriculaCommandValidation()
        {
            RuleFor(c => c.AlunoId)
                .NotEqual(Guid.Empty)
                .WithMessage(AlunoId);
            RuleFor(c => c.CursoId)
                .NotEqual(Guid.Empty)
                .WithMessage(CursoId);
            RuleFor(c => c.NomeCurso)
                .NotEmpty()
                .WithMessage(NomeCurso);
        }
    }
}
