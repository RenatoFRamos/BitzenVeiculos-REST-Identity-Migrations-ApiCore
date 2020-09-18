using System;
using System.ComponentModel.DataAnnotations;

namespace Bitzen.Veiculos.Business.Models
{
    public class Oportunidade : Entity
    {
        public Guid VeiculoId { get; set; }
        public Guid VendedorId { get; set; }
        public decimal Valor { get; set; }
        public decimal Comissao { get; set; }
        public EStatusOportunidade Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataExpiracao { get; set; }
        public bool Excluido { get; set; }
        public DateTime DataExclusao { get; set; }

      //  [ScaffoldColumn(false)]
       // public Veiculo Veiculo { get; set; }
    }
}