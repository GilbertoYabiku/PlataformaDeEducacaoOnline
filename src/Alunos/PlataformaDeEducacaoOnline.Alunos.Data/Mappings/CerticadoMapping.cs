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
    public class CertificadoMapping : IEntityTypeConfiguration<Certificado>
    {
        public void Configure(EntityTypeBuilder<Certificado> builder)
        {
            builder.ToTable("Certificados");
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Aluno);
            builder.HasOne(c => c.Matricula);
        }
    }
}
