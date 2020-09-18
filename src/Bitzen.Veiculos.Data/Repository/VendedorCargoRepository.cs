using Bitzen.Veiculos.Business.Interfaces;
using Bitzen.Veiculos.Business.Models;
using Bitzen.Veiculos.Data.Context;

namespace Bitzen.Veiculos.Data.Repository
{
    public class VendedorCargoRepository : Repository<VendedorCargo>, IVendedorCargoRepository
    {
        public VendedorCargoRepository(DataContext db) : base(db)
        {
        }
    }
}