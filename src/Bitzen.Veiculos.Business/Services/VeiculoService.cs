using Bitzen.Veiculos.Business.Interfaces;
using Bitzen.Veiculos.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitzen.Veiculos.Business.Services
{
    public class VeiculoService : BaseService, IVeiculoService
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IUser _user;

        public VeiculoService(INotificador notificador,
                              IVeiculoRepository veiculoRepository,
                              IUser user) : base(notificador)
        {
            _veiculoRepository = veiculoRepository;
            _user = user;
        }

        public async Task<Veiculo> Adicionar(Veiculo veiculo)
        {
            veiculo = InserirDadosIniciais(veiculo);

            if (!ExecutarValidacao(new VeiculoValidation(), veiculo))
                return veiculo;

            await _veiculoRepository.Adicionar(veiculo);

            return veiculo;
        }

        public async Task Atualizar(Veiculo veiculo)
        {
            if (!ExecutarValidacao(new VeiculoValidation(), veiculo))
                return;

            if (VerificaSeMarcadoComoExcluido(veiculo))
            {
                Notificar("Este Veiculo já foi excluido!");
            }

            await _veiculoRepository.Atualizar(veiculo);
        }

        public async Task Remover(Guid id)
        {
            IEnumerable<Veiculo> veiculos = await _veiculoRepository.Buscar(v => v.Id == id &&
                                                                            v.Excluido == false);

            if (veiculos.Any())
            {
                Veiculo veiculo = veiculos.FirstOrDefault();

                veiculo.Excluido = true;
                veiculo.DataExclusao = DateTime.Now;
                await _veiculoRepository.Atualizar(veiculo);
            }
            else
            {
                Notificar("Id não encontrado para exclusão!");
            }
        }

        private bool VerificaSeMarcadoComoExcluido(Veiculo veiculo)
        {
            if (veiculo.Excluido)
                return true;

            return false;
        }

        private Veiculo InserirDadosIniciais(Veiculo veiculo)
        {
            veiculo.Status = EStatusVeiculo.Disponivel;
            veiculo.Excluido = false;

            if (veiculo.Id.Equals(Guid.Empty))
                veiculo.Id = Guid.NewGuid();

            return veiculo;
        }

        public void Dispose()
        {
            _veiculoRepository.Dispose();
        }
    }
}
