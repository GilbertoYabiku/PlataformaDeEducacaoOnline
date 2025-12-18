using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using PlataformaDeEducacaoOnline.Core.Bus;

namespace PlataformaDeEducacaoOnline.Alunos.Application.Commands
{
    public class AdicionarAdminCommand : Command
    {
        public string UsuarioId { get; set; }

        public AdicionarAdminCommand(string usuarioId)
        {
            if (Guid.TryParse(usuarioId, out var parsedGuid))
            {
                AggregateId = parsedGuid;
            }
            UsuarioId = usuarioId;
        }

        public override bool CommandValido()
        {
            ValidationResult = new AdicionarAdminCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class AdicionarAdminCommandValidation : AbstractValidator<AdicionarAdminCommand>
    {
        public static string IdErro => "O campo UsuarioId é obrigatório";
        public AdicionarAdminCommandValidation()
        {
            RuleFor(c => c.UsuarioId)
                .NotEmpty()
                .WithMessage(IdErro);
        }
    }
}
