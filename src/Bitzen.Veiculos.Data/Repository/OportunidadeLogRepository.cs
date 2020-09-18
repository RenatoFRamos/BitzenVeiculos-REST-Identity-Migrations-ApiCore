using Bitzen.Veiculos.Business.Interfaces;
using Bitzen.Veiculos.Business.Models;
using Bitzen.Veiculos.Data.Context;

namespace Bitzen.Veiculos.Data.Repository
{
    public class OportunidadeLogRepository : Repository<OportunidadeLog>, IOportunidadeLogRepository
    {
        public OportunidadeLogRepository(DataContext db) : base(db)
        {
        }
    }
}