using System;
using FluentValidation;

namespace Bitzen.Veiculos.Business.Models.Validations
{
    public class OportunidadeValidation : AbstractValidator<Oportunidade>
    {
        public OportunidadeValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.VendedorId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.VeiculoId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.Status)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.DataExpiracao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .GreaterThan(DateTime.Now)
                .WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");

        }
    }
}
