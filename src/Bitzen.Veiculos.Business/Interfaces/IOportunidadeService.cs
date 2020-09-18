using Bitzen.Veiculos.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitzen.Veiculos.Business.Interfaces
{
    public interface IOportunidadeService : IDisposable
    {
        Task<Oportunidade> Adicionar(Oportunidade oportunidade);
        Task<bool> Atualizar( Oportunidade oportunidade);
        Task<bool> Remover  (Guid id);
        Task<IEnumerable<Oportunidade>> ObterTodos();
        Task<Oportunidade> ObterPorId(Guid id);
    }
}
