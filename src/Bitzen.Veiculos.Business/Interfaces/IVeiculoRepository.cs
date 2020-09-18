using Bitzen.Veiculos.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bitzen.Veiculos.Business.Interfaces
{
    public interface IVeiculoRepository : IRepository<Veiculo>
    {
        Task<IEnumerable<Veiculo>> ObterVeiculosPorNome(string nome);
        Task<IEnumerable<Veiculo>> ObterVeiculosPorPlaca(string placa);
        Task<IEnumerable<Veiculo>> ObterVeiculosPorAno(string ano);
        Task<IEnumerable<Veiculo>> ObterVeiculosPorModelo(string modelo);
        Task<IEnumerable<Veiculo>> ObterVeiculosPorCombustivel(string combustivel);
        Task<IEnumerable<Veiculo>> ObterVeiculosPorValores(decimal valorInicial, decimal valorFinal);
    }
}
