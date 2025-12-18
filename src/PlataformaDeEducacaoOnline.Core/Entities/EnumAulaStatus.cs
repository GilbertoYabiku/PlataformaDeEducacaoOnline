using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaDeEducacaoOnline.Core.Entities
{
    public enum EnumAulaStatus
    {
        [Description("Não Iniciada")]
        NaoIniciada = 0,
        [Description("Em Andamento")]
        EmAndamento = 1,
        [Description("Concluída")]
        Concluida = 2,
    }
}
