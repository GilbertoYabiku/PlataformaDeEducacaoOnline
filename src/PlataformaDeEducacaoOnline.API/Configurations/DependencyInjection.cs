using MediatR;
using PlataformaDeEducacaoOnline.Alunos.Application.Commands;
using PlataformaDeEducacaoOnline.Alunos.Application.Services;
using PlataformaDeEducacaoOnline.Alunos.Application.Services.Interfaces;
using PlataformaDeEducacaoOnline.Alunos.Data.Context;
using PlataformaDeEducacaoOnline.Alunos.Data.Repositories;
using PlataformaDeEducacaoOnline.Alunos.Data.Repositories.Interfaces;
using PlataformaDeEducacaoOnline.Conteudos.Application.Commands;
using PlataformaDeEducacaoOnline.Conteudos.Application.Services;
using PlataformaDeEducacaoOnline.Conteudos.Application.Services.Interfaces;
using PlataformaDeEducacaoOnline.Conteudos.Data.Context;
using PlataformaDeEducacaoOnline.Conteudos.Data.Repositories;
using PlataformaDeEducacaoOnline.Conteudos.Data.Repositories.Interfaces;
using PlataformaDeEducacaoOnline.Core.Bus.Notifications;
using PlataformaDeEducacaoOnline.Core.Entities;
using PlataformaDeEducacaoOnline.Financeiro.AntiCorruption.CartaoConnector;
using PlataformaDeEducacaoOnline.Financeiro.AntiCorruption.CartaoConnector.Interfaces;
using PlataformaDeEducacaoOnline.Financeiro.Application.Services;
using PlataformaDeEducacaoOnline.Financeiro.Application.Services.Interfaces;
using PlataformaDeEducacaoOnline.Financeiro.Data.Context;
using PlataformaDeEducacaoOnline.Financeiro.Data.Repository;

namespace PlataformaDeEducacaoOnline.API.Configurations;

public static class DependencyInjection
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
        builder.Services.AddScoped<IStatusMatriculaRepository, StatusMatriculaRepository>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        builder.Services.AddScoped<IAlunoService, AlunoService>();
        builder.Services.AddScoped<AlunosContext>();

        builder.Services.AddScoped<ICursoRepository, CursoRepository>();
        builder.Services.AddScoped<ICursoService, CursoService>();
        builder.Services.AddScoped<IAulaRepository, AulaRepository>();
        builder.Services.AddScoped<ConteudosContext>();

        builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<AdicionarAlunoCommand>());
        builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<AdicionarAulaCommand>());
        builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<PagamentoService>());

        builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
        builder.Services.AddScoped<IPagamentoService, PagamentoService>();
        builder.Services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
        builder.Services.AddScoped<ICartaoGateway, CartaoGateway>();
        builder.Services.AddScoped<FinanceiroContext>();

        builder.Services.AddScoped<IAppIdentityUser, AppIdentityUser>();

        return builder;

    }
}