using Bitzen.Veiculos.Business.Interfaces;
using Bitzen.Veiculos.Business.Models;
using Bitzen.Veiculos.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitzen.Veiculos.Business.Services
{
    public class CargoService : BaseService, ICargoService
    {
        private readonly ICargoRepository _cargoRepository;
        private readonly IUser _user;

        public CargoService(INotificador notificador,
                              ICargoRepository cargoRepository,
                              IUser user) : base(notificador)
        {
            _cargoRepository = cargoRepository;
            _user = user;
        }

        public async Task<Cargo> ObterPorId(Guid id)
        {
            return await _cargoRepository.ObterPorId(id);
        }

        public async Task<Cargo> Adicionar(Cargo cargo)
        {
            InsereIdCasoSejaNulo(cargo);

            if (!ExecutarValidacao(new CargoValidation(), cargo))
                return cargo;

            await _cargoRepository.Adicionar(cargo);

            return cargo;
        }

        public async Task Atualizar(Cargo Cargo)
        {
            if (!ExecutarValidacao(new CargoValidation(), Cargo))
                return;

            await _cargoRepository.Atualizar(Cargo);
        }

        public async Task Remover(Guid id)
        {
            IEnumerable<Cargo> Cargos = await _cargoRepository.Buscar(o => o.Id == id);

            if (Cargos.Any())
            {
                Cargos = null;
                await _cargoRepository.Remover(id);
            }
            else
            {
                Notificar("Id não encontrado para exclusão!");
            }
        }

        protected Cargo InsereIdCasoSejaNulo(Cargo cargo)
        {
            if (cargo.Id.Equals(Guid.Empty))
                cargo.Id = Guid.NewGuid();

            return cargo;
        }

        public void Dispose()
        {
            _cargoRepository.Dispose();
        }
    }
}
