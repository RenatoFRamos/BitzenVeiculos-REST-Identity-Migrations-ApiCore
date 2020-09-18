using Bitzen.Veiculos.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bitzen.Veiculos.Api.ViewModels
{
    public class OportunidadeLogViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid OportunidadeId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid VendedorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid VeiculoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Comissao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public EStatusOportunidade Status { get; set; }

        public DateTime DataEvento { get; set; }

        public bool Excluido { get; set; }

    }
}