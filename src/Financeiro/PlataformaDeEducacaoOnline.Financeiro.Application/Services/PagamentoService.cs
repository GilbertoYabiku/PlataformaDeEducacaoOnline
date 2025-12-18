using MediatR;
using PlataformaDeEducacaoOnline.Core.Bus.Notifications;
using PlataformaDeEducacaoOnline.Core.Entities;
using PlataformaDeEducacaoOnline.Financeiro.Application.Models;
using PlataformaDeEducacaoOnline.Financeiro.Application.Services.Interfaces;

namespace PlataformaDeEducacaoOnline.Financeiro.Application.Services;

public class PagamentoService(IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
                              IPagamentoRepository pagamentoRepository,
                              IMediator mediator) : IPagamentoService
{
    public async Task<bool> RealizarPagamentoCurso(PagamentoCurso pagamentoCurso)
    {
        var pedido = new Pedido
        {
            CursoId = pagamentoCurso.CursoId,
            AlunoId = pagamentoCurso.AlunoId,
            Valor = pagamentoCurso.Total,
        };

        var pagamento = new Pagamento
        {
            Valor = pagamentoCurso.Total,
            NomeCartao = pagamentoCurso.NomeCartao,
            NumeroCartao = pagamentoCurso.NumeroCartao,
            ExpiracaoCartao = pagamentoCurso.ExpiracaoCartao,
            CvvCartao = pagamentoCurso.CvvCartao,
            AlunoId = pagamentoCurso.AlunoId,
            CursoId = pagamentoCurso.CursoId
        };

        var transacao = pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

        if (transacao.StatusTransacao == StatusTransacao.Pago)
        {
            pagamentoRepository.Adicionar(pagamento);
            pagamentoRepository.AdicionarTransacao(transacao);

            await pagamentoRepository.UnitOfWork.Commit();
            return true;
        }

        await mediator.Publish(new DomainNotification("pagamento", "A operadora recusou o pagamento"));
        return false;
    }
}