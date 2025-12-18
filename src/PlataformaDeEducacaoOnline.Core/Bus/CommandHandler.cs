using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaDeEducacaoOnline.Core.Bus
{
    public abstract class CommandHandler
    {
        protected virtual Task AdicionarNotificacao(string type, string descricao, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        protected virtual void AdicionarNotificacao(string type, string descricao)
        {
            throw new NotImplementedException();
        }
    }
}
