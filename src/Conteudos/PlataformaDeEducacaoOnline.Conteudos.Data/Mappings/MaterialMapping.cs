using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaDeEducacaoOnline.Conteudos.Domain.Entities;

namespace PlataformaDeEducacaoOnline.Conteudos.Data.Mappings;

public class MaterialMapping : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.ToTable("Materiais");

        builder.HasKey(m => m.Id);
    }
}