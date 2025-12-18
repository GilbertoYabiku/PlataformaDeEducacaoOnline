using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaDeEducacaoOnline.Alunos.Application.Commands;
using PlataformaDeEducacaoOnline.Alunos.Application.Services.Interfaces;
using PlataformaDeEducacaoOnline.API.Entities;
using PlataformaDeEducacaoOnline.Conteudos.Application.Commands;
using PlataformaDeEducacaoOnline.Conteudos.Application.Services.Interfaces;
using PlataformaDeEducacaoOnline.Conteudos.Application.ViewModels;
using PlataformaDeEducacaoOnline.Conteudos.Data.Repositories.Interfaces;
using PlataformaDeEducacaoOnline.Core.Bus.Notifications;
using PlataformaDeEducacaoOnline.Core.Entities;
using System.Net;

namespace PlataformaDeEducacaoOnline.API.Controllers
{
    [Route("api/cursos")]
    public class CursosController(INotificationHandler<DomainNotification> notificacoes,
                            IMediator mediator,
                            IAppIdentityUser identityUser,
                            IAlunoService alunoService,
                            ICursoRepository cursoRepository,
                            ICursoService cursoService) : MainController(notificacoes, mediator, identityUser)
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoViewModel>>> ObterTodos()
        {
            var cursos = await cursoService.ObterTodos();
            return RespostaPadrao(HttpStatusCode.OK, cursos);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CursoViewModel>> ObterPorId(Guid id)
        {
            var curso = await cursoService.ObterPorId(id);
            if (curso is null)
            {
                NotificarErro("Curso", "Curso não encontrado.");
                return RespostaPadrao();
            }

            return RespostaPadrao(HttpStatusCode.OK, curso);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CursoNovoDto curso)
        {   
            var command = new AdicionarCursoCommand(curso.Nome, curso.Conteudo, UsuarioId, curso.Preco);
            await _mediator.Send(command);

            return RespostaPadrao(HttpStatusCode.Created);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Curso curso)
        {
            if (id != curso.Id)
            {
                NotificarErro("Curso", "O ID do curso não pode ser diferente do ID informado na URL.");
                return RespostaPadrao();
            }
            var command = new AtualizarCursoCommand(curso.Id, curso.Nome, curso.Conteudo, curso.Preco);

            await _mediator.Send(command);
            return RespostaPadrao(HttpStatusCode.NoContent);
        }

        [Authorize(Roles = "ALUNO")]
        [HttpPost("{id:guid}/concluir-curso")]
        public async Task<IActionResult> ConcluirCurso(Guid id)
        {   
            var curso = await cursoService.ObterPorId(id);

            await ValidarConclusaoCurso(curso);

            if (!OperacaoValida())
                return RespostaPadrao();

            var command = new ConcluirMatriculaCommand(UsuarioId, id, curso.Nome);
            await _mediator.Send(command);

            return RespostaPadrao(HttpStatusCode.Created);
        }

        [Authorize(Roles = "ALUNO")]
        [HttpPost("{cursoId:guid}/realizar-pagamento")]
        public async Task<IActionResult> RealizarPagamento(Guid cursoId, [FromBody] DadosPagamento dadosPagamento)
        {
            var curso = await cursoService.ObterPorId(cursoId);

            await ValidarCursoMatricula(curso);

            if (!OperacaoValida())
                return RespostaPadrao();

            var command = new RealizarPagamentoCursoCommand(UsuarioId, cursoId, dadosPagamento.CvvCartao, dadosPagamento.ExpiracaoCartao, dadosPagamento.NomeCartao, dadosPagamento.NumeroCartao, curso.Preco);

            await _mediator.Send(command);

            return RespostaPadrao(HttpStatusCode.Created);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var command = new DeletarCursoCommand(id);
            await _mediator.Send(command);
            return RespostaPadrao(HttpStatusCode.NoContent);
        }

        private async Task ValidarCursoMatricula(CursoViewModel? curso)
        {
            if (curso is null)
            {
                NotificarErro("Curso", "Curso não encontrado.");
                return;
            }
            var matricula = await alunoService.ObterMatricula(curso.Id, UsuarioId);

            if (matricula is not { Status: (int)EnumMatricula.AguardandoPagamento })
            {
                NotificarErro( "Matricula", "A matrícula deve estar com status 'Aguardando Pagamento' para realizar o pagamento.");
            }
        }

        private async Task ValidarConclusaoCurso(CursoViewModel? curso)
        {
            if (curso is null)
            {
                NotificarErro("Curso", "Curso não encontrado.");
                return;
            }
            var progressoCurso = await cursoRepository.ObterProgressoCurso(curso.Id, UsuarioId);

            if (progressoCurso is null || !progressoCurso.CursoConcluido)
            {
                NotificarErro("Curso", "Todas as aulas deste curso precisam estar concluídas.");
            }
        }
    }
}
