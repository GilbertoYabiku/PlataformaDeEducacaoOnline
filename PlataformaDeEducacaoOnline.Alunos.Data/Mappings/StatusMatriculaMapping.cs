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
    public class StatusMatriculaMapping : IEntityTypeConfiguration<StatusMatricula>
    {
        public void Configure(EntityTypeBuilder<StatusMatricula> builder)
        {
            builder.HasKey(s => s.Id);
            builder.ToTable("StatusMatriculas");
        }
    }
}