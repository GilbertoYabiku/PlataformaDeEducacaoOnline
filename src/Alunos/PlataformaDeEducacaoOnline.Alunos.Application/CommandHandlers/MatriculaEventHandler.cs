using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PlataformaDeEducacaoOnline.Alunos.Application.Commands;
using PlataformaDeEducacaoOnline.Core.Bus.Events;

namespace PlataformaDeEducacaoOnline.Alunos.Application.CommandHandlers
{
    public class MatriculaEventHandler(IMediator mediator)
    : INotificationHandler<PagamentoRealizadoEvent>,
      INotificationHandler<MatriculaConcluidaEvent>
    {
        public async Task Handle(PagamentoRealizadoEvent notification, CancellationToken cancellationToken)
        {
            await mediator.Send(new AtivarMatriculaCommand(notification.AlunoId, notification.CursoId), cancellationToken);
        }

        public async Task Handle(MatriculaConcluidaEvent notification, CancellationToken cancellationToken)
        {
            await mediator.Send(new AdicionarCertificadoCommand(notification.AlunoId, notification.MatriculaId, notification.CursoId, notification.NomeCurso), cancellationToken);
        }
    }
}
