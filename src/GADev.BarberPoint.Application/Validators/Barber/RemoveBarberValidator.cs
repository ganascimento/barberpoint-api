using FluentValidation;
using GADev.BarberPoint.Application.Commands.Barber;

namespace GADev.BarberPoint.Application.Validators.Barber
{
    public class RemoveBarberValidator : AbstractValidator<RemoveBarberCommand>
    {
        public RemoveBarberValidator()
        {
            RuleFor(a => a.Id)
                .NotNull().WithMessage("O 'Id' não pode ser nulo")
                .GreaterThan(0).WithMessage("O campo 'Id' deve ser maior que 0");
        }
    }
}