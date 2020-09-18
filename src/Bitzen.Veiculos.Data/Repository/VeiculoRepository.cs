using Bitzen.Veiculos.Business.Interfaces;
using Bitzen.Veiculos.Business.Models;
using Bitzen.Veiculos.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bitzen.Veiculos.Data.Repository
{
    public class VeiculoRepository : Repository<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(DataContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Veiculo>> ObterVeiculosPorNome(string nome)
        {
            return await Db.Veiculos.AsNoTracking()
                .Where(v => v.Nome == nome
                 && v.Excluido == false
                 && v.Status == EStatusVeiculo.Disponivel)
                .OrderBy(v => v.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> ObterVeiculosPorPlaca(string placa)
        {
            return await Db.Veiculos.AsNoTracking()
                .Where(v => v.Placa == placa
                 && v.Excluido == false
                 && v.Status == EStatusVeiculo.Disponivel)
                .OrderBy(v => v.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> ObterVeiculosPorAno(string ano)
        {
            return await Db.Veiculos.AsNoTracking()
                .Where(v => v.Ano == ano
                 && v.Excluido == false
                 && v.Status == EStatusVeiculo.Disponivel)
                .OrderBy(v => v.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> ObterVeiculosPorModelo(string modelo)
        {
            return await Db.Veiculos.AsNoTracking()
                .Where(v => v.Modelo == modelo
                 && v.Excluido == false
                 && v.Status == EStatusVeiculo.Disponivel)
                .OrderBy(v => v.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> ObterVeiculosPorCombustivel(string combustivel)
        {
            return await Db.Veiculos.AsNoTracking()
                .Where(v => v.Combustivel == combustivel
                 && v.Excluido == false
                 && v.Status == EStatusVeiculo.Disponivel)
                .OrderBy(v => v.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> ObterVeiculosPorValores(decimal valorInicial, decimal valorFinal)
        {
            return await Db.Veiculos.AsNoTracking()
                .Where(v => (v.Valor >= valorInicial 
                 && v.Valor <= valorFinal
                 && v.Excluido == false
                 && v.Status == EStatusVeiculo.Disponivel))
                .OrderBy(v => v.Nome).ToListAsync();
        }
    }
}