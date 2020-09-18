using Bitzen.Veiculos.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bitzen.Veiculos.Business.Interfaces
{
    public interface ICargoService : IDisposable
    {
        Task<Cargo> Adicionar(Cargo cargo);
        Task Atualizar(Cargo cargo);
        Task Remover(Guid id);
        Task<Cargo> ObterPorId(Guid id);
    }
}
