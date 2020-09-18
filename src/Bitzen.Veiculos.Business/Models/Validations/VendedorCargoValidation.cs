using System;
using FluentValidation;

namespace Bitzen.Veiculos.Business.Models.Validations
{
    public class VendedorCargoValidation : AbstractValidator<VendedorCargo>
    {
        public VendedorCargoValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.VendedorId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        }
    }
}
