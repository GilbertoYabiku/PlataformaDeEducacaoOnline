using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlataformaDeEducacaoOnline.Core.Entities;

namespace PlataformaDeEducacaoOnline.Alunos.Domain.Entities
{
    public class Matricula : BaseEntity
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public DateTime? DataMatricula { get; private set; }
        public DateTime? DataConclusao { get; private set; }
        public Guid StatusId { get; private set; }

        // Ef relationship
        public Aluno Aluno { get; set; }
        public StatusMatricula Status { get; private set; }

        // Ef Constructor
        protected Matricula() { }
        public Matricula(Guid alunoId, Guid cursoId, StatusMatricula status)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
        }

    }
}
