using Bitzen.Veiculos.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bitzen.Veiculos.Business.Interfaces
{
    public interface IVendedorCargoService : IDisposable
    {
        Task<VendedorCargo> Adicionar(VendedorCargo vendedorCargo);
        Task Atualizar(VendedorCargo vendedorCargo);
        Task Remover(Guid id);
        Task<VendedorCargo> ObterPorVendedorId(Guid id);
    }
}
