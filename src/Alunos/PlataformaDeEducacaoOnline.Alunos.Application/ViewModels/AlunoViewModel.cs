using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaDeEducacaoOnline.Alunos.Application.ViewModels
{
    public class MatriculaViewModel
    {
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public int Status { get; set; }
        public string StatusDescricao { get; set; }
        public DateTime? DataMatricula { get; set; }
    }
}
