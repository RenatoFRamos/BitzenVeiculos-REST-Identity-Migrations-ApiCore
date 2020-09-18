using Bitzen.Veiculos.Business.Interfaces;
using Bitzen.Veiculos.Business.Models;
using Bitzen.Veiculos.Data.Context;

namespace Bitzen.Veiculos.Data.Repository
{
    public class OportunidadeRepository : Repository<Oportunidade>, IOportunidadeRepository
    {
        public OportunidadeRepository(DataContext db) : base(db)
        {
        }
    }
}