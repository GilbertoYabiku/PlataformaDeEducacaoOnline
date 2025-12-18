using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaDeEducacaoOnline.Alunos.Application.Commands;
using PlataformaDeEducacaoOnline.Alunos.Application.Services.Interfaces;
using PlataformaDeEducacaoOnline.API.Entities;
using PlataformaDeEducacaoOnline.Conteudos.Application.Services.Interfaces;
using PlataformaDeEducacaoOnline.Core.Bus.Notifications;
using PlataformaDeEducacaoOnline.Core.Entities;
using System.Net;

namespace PlataformaDeEducacaoOnline.API.Controllers;

[Authorize(Roles = "ADMIN,ALUNO")]
[Route("api/matriculas")]
public class MatriculasController(INotificationHandler<DomainNotification> notificacoes,
                                 IAlunoService alunoService,
                                 IAppIdentityUser identityUser,
                                 ICursoService cursoService,
                                 IMediator mediator) : MainController(notificacoes, mediator, identityUser)
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("pendentes-pagamento")]
    public async Task<ActionResult<IEnumerable<Matricula>>> ObterMatriculasPendentePagamento()
    {
        var matriculas = await alunoService.ObterMatriculasPendentePagamento(UsuarioId);
        return RespostaPadrao(HttpStatusCode.OK, matriculas);
    }

    [HttpPost("{cursoId:guid}")]
    public async Task<IActionResult> Adicionar(Guid cursoId)
    {   
        var curso = await cursoService.ObterPorId(cursoId);

        if (curso is null)
        {
            NotificarErro("Curso", "Curso não encontrado.");
            return RespostaPadrao();
        }
        var command = new AdicionarMatriculaCommand(UsuarioId, cursoId);
        await _mediator.Send(command);

        return RespostaPadrao(HttpStatusCode.Created);
    }
}