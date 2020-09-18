using Bitzen.Veiculos.Business.Models;
using System;
using System.Threading.Tasks;

namespace Bitzen.Veiculos.Business.Interfaces
{
    public interface IVeiculoService : IDisposable
    {
        Task<Veiculo> Adicionar(Veiculo veiculo);
        Task Atualizar(Veiculo veiculo);
        Task Remover(Guid id);
    }
}
