using FluentValidation;
using GADev.BarberPoint.Application.Commands.BarberShop;

namespace GADev.BarberPoint.Application.Validators.BarberShop
{
    public class RemoveBarberShopValidator : AbstractValidator<RemoveBarberShopCommand>
    {
        public RemoveBarberShopValidator()
        {
            RuleFor(a => a.Id)
                .NotNull().WithMessage("O campo 'Id' não pode ser nulo")
                .GreaterThan(0).WithMessage("O campo 'Id' deve ser maior que 0");
        }
    }
}