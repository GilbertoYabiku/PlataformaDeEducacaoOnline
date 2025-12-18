
using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlataformaDeEducacaoOnline.Conteudos.Data.Extensions;
using PlataformaDeEducacaoOnline.Core.Bus;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Conteudos.Data.Context;

public class ConteudosContext(DbContextOptions<ConteudosContext> options,
                                    IMediator mediator) : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var property in builder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetProperties()
                         .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(200)");

        builder.ApplyConfigurationsFromAssembly(typeof(ConteudosContext).Assembly);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType) &&
                !entityType.ClrType.IsAbstract && entityType.BaseType == null)
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(BaseEntity.DataExclusao));
                var condition = Expression.Equal(property, Expression.Constant(null));
                var lambda = Expression.Lambda(condition, parameter);

                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        foreach (var relationShip in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationShip.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }

        builder.Ignore<Event>();

        base.OnModelCreating(builder);
    }

    public async Task<bool> Commit()
    {   
        var sucesso = await SaveChangesAsync() > 0;
        
        if (sucesso)
            await mediator.PublishDomainEvents(this);

        return sucesso;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entityEntry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property("DataCriacao").CurrentValue = DateTime.Now;
                entityEntry.Property("DataAlteracao").IsModified = false;
                entityEntry.Property("DataExclusao").IsModified = false;
            }
            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                entityEntry.Property("DataCriacao").IsModified = false;
                entityEntry.Property("DataExclusao").IsModified = false;
            }
            if (entityEntry.State == EntityState.Deleted)
            {
                entityEntry.State = EntityState.Modified;
                entityEntry.Property("DataExclusao").CurrentValue = DateTime.Now;
                entityEntry.Property("DataCriacao").IsModified = false;
                entityEntry.Property("DataAlteracao").IsModified = false;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}