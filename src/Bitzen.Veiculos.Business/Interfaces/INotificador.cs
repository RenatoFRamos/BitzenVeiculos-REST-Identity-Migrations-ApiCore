using Bitzen.Veiculos.Business.Notifications;
using System.Collections.Generic;

namespace Bitzen.Veiculos.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
