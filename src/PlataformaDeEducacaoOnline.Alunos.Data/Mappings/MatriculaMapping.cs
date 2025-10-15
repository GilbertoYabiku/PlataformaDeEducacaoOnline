using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PlataformaDeEducacaoOnline.Alunos.Domain.Entities;

namespace PlataformaDeEducacaoOnline.Alunos.Data.Mappings
{
    public class MatriculaMapping : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("Matriculas");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.CursoId)
                .IsRequired();

            builder.HasOne(m => m.Status)
                .WithMany()
                .HasForeignKey(m => m.StatusId);
        }
    }
}
