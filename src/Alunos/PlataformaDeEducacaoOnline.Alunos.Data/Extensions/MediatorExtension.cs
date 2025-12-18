using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Alunos.Data.Extensions;

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