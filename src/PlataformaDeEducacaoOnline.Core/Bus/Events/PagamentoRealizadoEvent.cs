using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaDeEducacaoOnline.Core.Bus.Events
{
    public class PagamentoRealizadoEvent(Guid cursoId, Guid alunoId) : Event
    {
        public Guid CursoId { get; set; } = cursoId;
        public Guid AlunoId { get; set; } = alunoId;
    }
}
