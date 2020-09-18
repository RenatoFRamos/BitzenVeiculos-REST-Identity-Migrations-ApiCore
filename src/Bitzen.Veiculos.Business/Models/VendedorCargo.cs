using System;

namespace Bitzen.Veiculos.Business.Models
{
    public class VendedorCargo : Entity
    {
        public Guid VendedorId { get; set; }

        public Guid CargoId { get; set; }

        public decimal Comissao { get; set; }
    }
}