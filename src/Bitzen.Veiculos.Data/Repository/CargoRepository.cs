using Bitzen.Veiculos.Business.Interfaces;
using Bitzen.Veiculos.Business.Models;
using Bitzen.Veiculos.Data.Context;

namespace Bitzen.Veiculos.Data.Repository
{
    public class CargoRepository : Repository<Cargo>, ICargoRepository
    {
        public CargoRepository(DataContext db) : base(db)
        {
        }
    }
}