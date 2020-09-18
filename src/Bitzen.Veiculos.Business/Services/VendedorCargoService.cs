using Bitzen.Veiculos.Business.Interfaces;
using Bitzen.Veiculos.Business.Models;
using Bitzen.Veiculos.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitzen.Veiculos.Business.Services
{
    public class VendedorCargoService : BaseService, IVendedorCargoService
    {
        private readonly IVendedorCargoRepository _vendedorCargoRepository;
        private readonly IUser _user;

        public VendedorCargoService(INotificador notificador,
                              IVendedorCargoRepository vendedorCargoRepository,
                              IUser user) : base(notificador)
        {
            _vendedorCargoRepository = vendedorCargoRepository;
            _user = user;
        }

        public async Task<VendedorCargo> ObterPorVendedorId(Guid id)
        {
            var vendedorCargo = await _vendedorCargoRepository.Buscar(v => v.VendedorId == id);
            return vendedorCargo.FirstOrDefault();
        }

        public async Task<VendedorCargo> Adicionar(VendedorCargo vendedorCargo)
        {
            vendedorCargo = InsereIdCasoSejaNulo(vendedorCargo);

            if (!ExecutarValidacao(new VendedorCargoValidation(), vendedorCargo))
                return vendedorCargo;

            await _vendedorCargoRepository.Adicionar(vendedorCargo);

            return vendedorCargo;
        }

        public async Task Atualizar(VendedorCargo vendedorCargo)
        {
            if (!ExecutarValidacao(new VendedorCargoValidation(), vendedorCargo))
                return;

            await _vendedorCargoRepository.Atualizar(vendedorCargo);
        }

        public async Task Remover(Guid id)
        {
            IEnumerable<VendedorCargo> vendedoresCargo = await _vendedorCargoRepository.Buscar(o => o.Id == id);

            if (vendedoresCargo.Any())
            {
                VendedorCargo vendedorCargo = vendedoresCargo.FirstOrDefault();
                await _vendedorCargoRepository.Atualizar(vendedorCargo);
            }
            else
            {
                Notificar("Id não encontrado para exclusão!");
            }
        }

        protected VendedorCargo InsereIdCasoSejaNulo(VendedorCargo vendedorCargo)
        {
            if (vendedorCargo.Id.Equals(Guid.Empty))
                vendedorCargo.Id = Guid.NewGuid();

            return vendedorCargo;
        }

        public void Dispose()
        {
            _vendedorCargoRepository.Dispose();
        }
    }
}
