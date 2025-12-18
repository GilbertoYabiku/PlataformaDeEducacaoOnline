using MediatR;
using PlataformaDeEducacaoOnline.Core.Entities;
using PlataformaDeEducacaoOnline.Financeiro.Data.Context;

namespace PlataformaDeEducacaoOnline.Financeiro.Data.Extensions;

public static class MediatorExtension
{
    public static async Task PublishDomainEvents(this IMediator mediator, FinanceiroContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<BaseEntity>()
            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notificacoes)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.LimparEventos());

        var tasks = domainEvents
            .Select(async (domainEvent) => {
                await mediator.Publish(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}