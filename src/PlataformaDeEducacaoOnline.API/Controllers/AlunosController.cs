using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaDeEducacaoOnline.Alunos.Application.Services.Interfaces;
using PlataformaDeEducacaoOnline.Conteudos.Application.Services.Interfaces;
using PlataformaDeEducacaoOnline.Core.Bus.Notifications;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.API.Controllers;

[Route("api/alunos")]
public class AlunosController(INotificationHandler<DomainNotification> notificacoes,
                            IAlunoService alunoService,
                            ICursoService cursoService,
                            IAppIdentityUser identityUser,
                             IMediator mediator) : MainController(notificacoes, mediator, identityUser)
{

    [Authorize(Roles = "ALUNO")]
    [HttpGet("certificados/{id:guid}/download")]
    public async Task<IActionResult> BaixarCertificado(Guid id)
    {
        var certificado = await alunoService.ObterCertificado(id, UsuarioId);
        if (certificado?.Arquivo == null || certificado.Arquivo.Length == 0)
        {
            return BadRequest();
        }

        return File(certificado.Arquivo, "application/pdf", "certificado.pdf");
    }

    [Authorize(Roles = "ALUNO")]
    [HttpGet("historico-aprendizagem/{cursoId:guid}")]
    public async Task<IActionResult> ObterHistoricoAprendizagem(Guid cursoId)
    {
        var historico = await cursoService.ObterHistoricoAprendizagem(cursoId, UsuarioId);

        if (historico == null)
        {   
            NotificarErro("HistoricoAprendizagem", "Não foi encontrado nenhum histórico para o curso informado.");
            return RespostaPadrao();
        }
        return RespostaPadrao(data: historico);
    }
}