using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PlataformaDeEducacaoOnline.Alunos.Application.Commands;
using PlataformaDeEducacaoOnline.Alunos.Data.Repositories.Interfaces;
using PlataformaDeEducacaoOnline.Alunos.Data.Repositories;
using PlataformaDeEducacaoOnline.Alunos.Domain.Entities;
using PlataformaDeEducacaoOnline.Core.Bus.Notifications;
using PlataformaDeEducacaoOnline.Core.Bus;

namespace PlataformaDeEducacaoOnline.Alunos.Application.CommandHandlers
{
    public class UsuarioCommandHandler(IMediator mediator,
                                   IAlunoRepository alunoRepository,
                                   IUsuarioRepository usuarioRepository) : CommandHandler,
                                    IRequestHandler<AdicionarAlunoCommand, bool>,
                                    IRequestHandler<AdicionarAdminCommand, bool>
    {
        public async Task<bool> Handle(AdicionarAlunoCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var aluno = new Aluno(Guid.Parse(request.UsuarioId), request.Nome);

            alunoRepository.Adicionar(aluno);
            return await alunoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AdicionarAdminCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var usuario = new Usuario(Guid.Parse(request.UsuarioId));

            usuarioRepository.Adicionar(usuario);
            return await usuarioRepository.UnitOfWork.Commit();
        }

        private bool ValidarComando(Command command)
        {
            if (command.CommandValido()) return true;
            foreach (var erro in command.ValidationResult.Errors)
            {
                mediator.Publish(new DomainNotification(command.MessageType, erro.ErrorMessage));
            }
            return false;
        }
    }
}
