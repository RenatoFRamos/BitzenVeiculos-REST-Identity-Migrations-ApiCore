using Bitzen.Veiculos.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitzen.Veiculos.Business.Interfaces
{
    public interface IOportunidadeLogService : IDisposable
    {
        Task Adicionar(OportunidadeLog oportunidadeLog);
        Task Atualizar(OportunidadeLog oportunidadeLog);
        Task Remover(Guid id);
    }
}
