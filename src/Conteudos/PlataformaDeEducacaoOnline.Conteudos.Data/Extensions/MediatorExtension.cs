using MediatR;
using Microsoft.EntityFrameworkCore;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Conteudos.Data.Extensions;

public static class MediatorExtension
{
    public static async Task PublishDomainEvents(this IMediator mediator, DbContext context)
    {
        var domainEntities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any())
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = domainEntities.SelectMany(x => x.Notificacoes).ToList();

        domainEntities.ForEach(entity => entity.LimparEventos());

        var tasks = domainEvents.Select(async (domainEvent) =>
        {
            await mediator.Publish(domainEvent);
        });

        await Task.WhenAll(tasks);
    }
}