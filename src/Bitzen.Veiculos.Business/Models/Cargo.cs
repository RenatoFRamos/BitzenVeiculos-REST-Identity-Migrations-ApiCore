using System;

namespace Bitzen.Veiculos.Business.Models
{
    public class Cargo : Entity
    {
        public string Nome { get; set; }
        public decimal Comissao { get; set; }
    }
}