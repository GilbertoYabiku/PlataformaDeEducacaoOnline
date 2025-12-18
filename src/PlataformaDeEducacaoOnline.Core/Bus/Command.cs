using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;

namespace PlataformaDeEducacaoOnline.Core.Bus
{
    public abstract class Command : Message, IRequest<bool>
    {
        public DateTime Timestamp { get; protected set; }
        public ValidationResult ValidationResult { get; set; }
        protected Command()
        {
            Timestamp = DateTime.Now;
        }
        public abstract bool CommandValido();
    }
}
