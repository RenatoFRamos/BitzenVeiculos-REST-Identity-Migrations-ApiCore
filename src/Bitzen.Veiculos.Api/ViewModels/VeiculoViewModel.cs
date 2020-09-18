using System;
using System.ComponentModel.DataAnnotations;
using Bitzen.Veiculos.Business.Models;

namespace Bitzen.Veiculos.Api.ViewModels
{
    public class VeiculoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(7, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 7)]
        public string Placa { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(4, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 4)]
        public string Ano { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 2)]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 2)]
        public string Combustivel { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public EStatusVeiculo Status { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [ScaffoldColumn(false)]
        public string Excluido { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataExclusao { get; set; }
    }
}