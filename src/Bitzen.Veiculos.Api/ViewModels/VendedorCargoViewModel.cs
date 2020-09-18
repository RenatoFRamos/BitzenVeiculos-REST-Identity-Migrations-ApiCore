using System;
using System.ComponentModel.DataAnnotations;

namespace Bitzen.Veiculos.Business.Models
{
    public class VendedorCargoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid VendedorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid CargoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Comissao { get; set; }
    }
}