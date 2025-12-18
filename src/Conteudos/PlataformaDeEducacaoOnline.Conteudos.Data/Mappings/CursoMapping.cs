using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeEducacaoOnline.Conteudos.Domain.Entities;

namespace PlataformaDeEducacaoOnline.Conteudos.Data.Mappings;

public class CursoMapping : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("Cursos");
        builder.HasKey(c => c.Id);

        // 1 : N - Curso : Aulas
        builder.HasMany(c => c.Aulas)
            .WithOne(c => c.Curso);
    }
}