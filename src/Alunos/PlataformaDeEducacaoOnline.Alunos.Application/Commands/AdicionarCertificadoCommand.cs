using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using PlataformaDeEducacaoOnline.Core.Bus;

namespace PlataformaDeEducacaoOnline.Alunos.Application.Commands
{
    public class AdicionarCertificadoCommand : Command
    {
        public Guid AlunoId { get; set; }
        public Guid MatriculaId { get; set; }
        public Guid CursoId { get; set; }
        public string NomeCurso { get; set; }

        public AdicionarCertificadoCommand(Guid alunoId, Guid matriculaId, Guid cursoId, string nomeCurso)
        {
            AggregateId = alunoId;
            AlunoId = alunoId;
            MatriculaId = matriculaId;
            CursoId = cursoId;
            NomeCurso = nomeCurso;
        }
        public override bool CommandValido()
        {
            ValidationResult = new GerarCertificadoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class GerarCertificadoCommandValidator : AbstractValidator<AdicionarCertificadoCommand>
    {
        public static string AlunoIdErro => "O campo AlunoId é obrigatório.";
        public static string MatriculaIdErro => "O campo MatriculaId é obrigatório.";
        public static string CursoIdErro => "O campo CursoId é obrigatório.";
        public static string NomeCursoErro => "O campo NomeCurso é obrigatório.";

        public GerarCertificadoCommandValidator()
        {
            RuleFor(c => c.AlunoId)
                .NotEqual(Guid.Empty)
                .WithMessage(AlunoIdErro);
            RuleFor(c => c.MatriculaId)
                .NotEqual(Guid.Empty)
                .WithMessage(MatriculaIdErro);
            RuleFor(c => c.CursoId)
                .NotEqual(Guid.Empty)
                .WithMessage(CursoIdErro);
            RuleFor(c => c.NomeCurso)
                .NotEmpty()
                .WithMessage(NomeCursoErro);
        }
    }
}
