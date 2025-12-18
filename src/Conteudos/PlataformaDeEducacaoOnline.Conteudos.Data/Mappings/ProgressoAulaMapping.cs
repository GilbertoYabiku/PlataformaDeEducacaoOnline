using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeEducacaoOnline.Conteudos.Domain.Entities;

namespace PlataformaDeEducacaoOnline.Conteudos.Data.Mappings;

public class ProgressoAulaMapping : IEntityTypeConfiguration<ProgressoAula>
{
    public void Configure(EntityTypeBuilder<ProgressoAula> builder)
    {
        builder.ToTable("ProgressoAulas");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.AlunoId)
            .IsRequired();

        builder.Property(p => p.AulaId)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasConversion<short>();

        builder.HasIndex(p => new { p.AulaId, p.AlunoId })
            .IsUnique(); ;
    }
}