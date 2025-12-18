using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaDeEducacaoOnline.Alunos.Application.Services.Interfaces;
using PlataformaDeEducacaoOnline.Alunos.Application.ViewModels;
using PlataformaDeEducacaoOnline.API.Entities;
using PlataformaDeEducacaoOnline.Conteudos.Application.Commands;
using PlataformaDeEducacaoOnline.Core.Bus.Notifications;
using PlataformaDeEducacaoOnline.Core.Entities;
using System.Net;

namespace PlataformaDeEducacaoOnline.API.Controllers;

[Route("api/cursos/{cursoId:guid}/aulas")]
public class AulasController(INotificationHandler<DomainNotification> notificacoes,
                            IAppIdentityUser identityUser,
                            IAlunoService alunoService,
                            IMediator mediator) : MainController(notificacoes, mediator, identityUser)
{
    private readonly IMediator _mediator = mediator;

    [Authorize(Roles = "ADMIN")]
    [HttpPost("adicionar-aula")]
    public async Task<IActionResult> Adicionar([FromBody] Aula aulaDto, Guid cursoId)
    {
        var command = new AdicionarAulaCommand(aulaDto.Nome, aulaDto.Conteudo, cursoId,
                                               aulaDto.NomeMaterial, aulaDto.TipoMaterial);

        await _mediator.Send(command);

        return RespostaPadrao(HttpStatusCode.Created);
    }

    [Authorize(Roles = "ALUNO")]
    [HttpPost("{id:guid}/realizar-aula")]
    public async Task<IActionResult> Realizar(Guid id, Guid cursoId)
    {
        var matricula = await alunoService.ObterMatricula(cursoId, UsuarioId);

        ValidarMatricula(matricula);

        if (!OperacaoValida())
            return RespostaPadrao();

        var command = new RealizarAulaCommand(id, UsuarioId, cursoId);
        await _mediator.Send(command);

        return RespostaPadrao(HttpStatusCode.Created);
    }

    [Authorize(Roles = "ALUNO")]
    [HttpPost("{id:guid}/concluir-aula")]
    public async Task<IActionResult> Concluir(Guid id, Guid cursoId)
    {
        var matricula = await alunoService.ObterMatricula(cursoId, UsuarioId);

        ValidarMatricula(matricula);

        if (!OperacaoValida())
            return RespostaPadrao();

        var command = new ConcluirAulaCommand(id, UsuarioId, cursoId);
        await _mediator.Send(command);
        
        return RespostaPadrao(HttpStatusCode.Created);
    }

    private void ValidarMatricula(MatriculaViewModel matricula)
    {
        if (matricula is null)
        {
            NotificarErro("Matricula", "Matrícula não encontrada.");
            return;
        }

        if (matricula?.Status != (int)EnumMatricula.Ativa && matricula?.Status != (int)EnumMatricula.Concluida)
        {
            NotificarErro("Matricula", "Matrícula não está ativa.");
        }
    }
}