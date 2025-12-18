using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaDeEducacaoOnline.Core.Entities
{
    public enum EnumMatricula : int
    {
        Iniciada,
        AguardandoPagamento = 1,
        Ativa = 2,
        Concluida = 3,
        Cancelada = 4,
    }
}
