using System;
using System.ComponentModel.DataAnnotations;

namespace Bitzen.Veiculos.Business.Models
{
    public class CargoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Comissao { get; set; }
    }
}