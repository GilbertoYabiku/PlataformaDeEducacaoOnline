using MediatR;
using PlataformaDeEducacaoOnline.Conteudos.Application.Commands;
using PlataformaDeEducacaoOnline.Core.Bus;
using PlataformaDeEducacaoOnline.Core.Bus.Notifications;
using PlataformaDeEducacaoOnline.Core.Entities;
using PlataformaDeEducacaoOnline.Financeiro.Application.Services.Interfaces;

namespace PlataformaDeEducacaoOnline.Conteudos.Application.CommandHandlers;

public class PagamentoCommandHandler(IPagamentoService pagamentoService, IMediator mediator): CommandHandler, IRequestHandler<RealizarPagamentoCursoCommand, bool>
{
    public async Task<bool> Handle(RealizarPagamentoCursoCommand command, CancellationToken cancellationToken)
    {
        if (!ValidarComando(command))
            return false;

        var pagamentoCurso = new PagamentoCurso
        {
            AlunoId = command.AlunoId,
            CursoId = command.CursoId,
            CvvCartao = command.CvvCartao,
            ExpiracaoCartao = command.ExpiracaoCartao,
            NomeCartao = command.NomeCartao,
            NumeroCartao = command.NumeroCartao,
            Total = command.Total
        };
        
        return await pagamentoService.RealizarPagamentoCurso(pagamentoCurso);
    }

    private bool ValidarComando(Command command)
    {
        if (command.CommandValido())
            return true;

        foreach (var erro in command.ValidationResult.Errors)
        {
            mediator.Publish(new DomainNotification(command.MessageType, erro.ErrorMessage), CancellationToken.None);
        }
        return false;
    }
}