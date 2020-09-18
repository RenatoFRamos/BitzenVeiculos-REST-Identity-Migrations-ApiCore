using System;

namespace Bitzen.Veiculos.Business.Models
{
    public class OportunidadeLog : Entity
    {
        public Guid OportunidadeId { get; set; }
        public Guid VeiculoId { get; set; }
        public Guid VendedorId { get; set; }
        public decimal Valor { get; set; }
        public decimal Comissao { get; set; }
        public EStatusOportunidade Status { get; set; }
        public DateTime DataEvento { get; set; }
        public bool Excluido { get; set; }
    }
}