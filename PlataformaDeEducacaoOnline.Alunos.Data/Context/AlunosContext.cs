using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlataformaDeEducacaoOnline.Core.Entities;
namespace PlataformaDeEducacaoOnline.Alunos.Data.Context
{
    public class AlunosContext(DbContextOptions<AlunosContext> options) : DbContext(options)
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetProperties()
                             .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(200)");

            builder.ApplyConfigurationsFromAssembly(typeof(AlunosContext).Assembly);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType) &&
                    !entityType.ClrType.IsAbstract && entityType.BaseType == null)
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(BaseEntity.DataDelecao));
                    var condition = Expression.Equal(property, Expression.Constant(null));
                    var lambda = Expression.Lambda(condition, parameter);

                    builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

            foreach (var relationShip in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationShip.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entityEntry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("DataCriacao").CurrentValue = DateTime.Now;
                    entityEntry.Property("DataAlteracao").IsModified = false;
                    entityEntry.Property("DataDelecao").IsModified = false;
                }
                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                    entityEntry.Property("DataCriacao").IsModified = false;
                    entityEntry.Property("DataDelecao").IsModified = false;
                }
                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.State = EntityState.Modified;
                    entityEntry.Property("DataExclusao").CurrentValue = DateTime.Now;
                    entityEntry.Property("DataCriacao").IsModified = false;
                    entityEntry.Property("DataDelecao").IsModified = false;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
