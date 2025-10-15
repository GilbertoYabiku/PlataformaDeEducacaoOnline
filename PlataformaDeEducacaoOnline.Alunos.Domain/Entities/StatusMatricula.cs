using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Alunos.Domain.Entities
{
    public class StatusMatricula : BaseEntity
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
