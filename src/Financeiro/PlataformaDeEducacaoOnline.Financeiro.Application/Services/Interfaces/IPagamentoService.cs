using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Financeiro.Application.Services.Interfaces;

public interface IPagamentoService
{
    Task<bool> RealizarPagamentoCurso(PagamentoCurso pagamentoCurso);
}