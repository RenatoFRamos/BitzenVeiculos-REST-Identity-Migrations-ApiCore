using System;
using FluentValidation;

namespace Bitzen.Veiculos.Business.Models.Validations
{
    public class CargoValidation : AbstractValidator<Cargo>
    {
        public CargoValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Comissao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .InclusiveBetween(5,15).WithMessage("O campo {PropertyName} precisa ter entre {From} e {To} caracteres");

        }
    }
}
