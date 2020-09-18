using System;

namespace Bitzen.Veiculos.Business.Models
{
    public class Veiculo : Entity
    {
        public string Nome { get; set; }
        public string Placa { get; set; }
        public string Ano { get; set; }
        public string Modelo { get; set; }
        public string Combustivel { get; set; }
        public EStatusVeiculo Status { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Excluido { get; set; }
        public DateTime DataExclusao { get; set; }
    }
}