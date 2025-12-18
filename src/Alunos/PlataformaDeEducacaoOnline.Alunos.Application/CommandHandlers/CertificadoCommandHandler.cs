using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PlataformaDeEducacaoOnline.Alunos.Application.Commands;
using PlataformaDeEducacaoOnline.Alunos.Domain.Entities;
using PlataformaDeEducacaoOnline.Core.Bus.Notifications;
using PlataformaDeEducacaoOnline.Core.Bus;
using PlataformaDeEducacaoOnline.Alunos.Data.Repositories.Interfaces;

namespace PlataformaDeEducacaoOnline.Alunos.Application.CommandHandlers
{
    public class CertificadoCommandHandler(IMediator mediator,
                                       IAlunoRepository alunoRepository) : CommandHandler,
                                        IRequestHandler<AdicionarCertificadoCommand, bool>
    {
        public async Task<bool> Handle(AdicionarCertificadoCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var aluno = await alunoRepository.ObterPorId(request.AlunoId);
            if (aluno is null)
            {
                await AdicionarNotificacao(request.MessageType, "Aluno não encontrado", cancellationToken);
                return false;
            }
            var matricula = await alunoRepository.ObterMatriculaPorCursoEAlunoId(request.CursoId, request.AlunoId);
            if (matricula is null)
            {
                await AdicionarNotificacao(request.MessageType, "Matrícula não encontrada", cancellationToken);
                return false;
            }

            if (!matricula.DataConclusao.HasValue)
            {
                await AdicionarNotificacao(request.MessageType, "Matrícula não está concluída", cancellationToken);
                return false;
            }

            var certificado = new Certificado(aluno.Nome, request.NomeCurso, matricula.Id, aluno.Id, matricula.DataConclusao);

            aluno.AdicionarCertificado(certificado);
            alunoRepository.AdicionarCertificado(certificado);

            return await alunoRepository.UnitOfWork.Commit();
        }
        protected override async Task AdicionarNotificacao(string messageType, string descricao, CancellationToken cancellationToken)
        {
            await mediator.Publish(new DomainNotification(messageType, descricao), cancellationToken);
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

}
