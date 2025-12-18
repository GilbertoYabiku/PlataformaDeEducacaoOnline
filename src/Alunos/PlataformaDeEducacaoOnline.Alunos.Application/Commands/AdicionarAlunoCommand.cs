using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using PlataformaDeEducacaoOnline.Core.Bus;

namespace PlataformaDeEducacaoOnline.Alunos.Application.Commands
{
    public class AdicionarAlunoCommand : Command
    {
        public string UsuarioId { get; set; }
        public string Nome { get; set; }

        public AdicionarAlunoCommand(string usuarioId, string nome)
        {
            if (Guid.TryParse(usuarioId, out var parsedGuid))
            {
                AggregateId = parsedGuid;
            }
            UsuarioId = usuarioId;
            Nome = nome;
        }

        public override bool CommandValido()
        {
            ValidationResult = new AdicionarAlunoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class AdicionarAlunoCommandValidation : AbstractValidator<AdicionarAlunoCommand>
    {
        public static string IdErro => "O campo UsuarioId é obrigatório";
        public static string NomeErro => "O campo Nome é obrigatório";

        public AdicionarAlunoCommandValidation()
        {
            RuleFor(c => c.UsuarioId)
                .NotEmpty()
                .WithMessage(IdErro);
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage(NomeErro);
        }
    }

}
