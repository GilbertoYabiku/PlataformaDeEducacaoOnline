using MediatR;
using PlataformaDeEducacaoOnline.Conteudos.Application.Commands;
using PlataformaDeEducacaoOnline.Conteudos.Application.Events;

namespace PlataformaDeEducacaoOnline.Conteudos.Application.CommandHandlers;

public class ProgressoEventHandler(IMediator mediator) : INotificationHandler<AulaConcluidaEvent>
{
    public async Task Handle(AulaConcluidaEvent notification, CancellationToken cancellationToken)
    {
        await mediator.Send(new AtualizarProgressoCursoCommand(notification.CursoId, notification.AlunoId), cancellationToken);
    }
}