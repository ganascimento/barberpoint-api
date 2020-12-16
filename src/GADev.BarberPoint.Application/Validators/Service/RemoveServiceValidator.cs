using FluentValidation;
using GADev.BarberPoint.Application.Commands.Service;

namespace GADev.BarberPoint.Application.Validators.Service
{
    public class RemoveServiceValidator : AbstractValidator<RemoveServiceCommand>
    {
        public RemoveServiceValidator()
        {
            RuleFor(a => a.Id)
                .NotNull().WithMessage("O 'Id' não pode ser nulo")
                .GreaterThan(0).WithMessage("O campo 'Id' deve ser maior que 0");
        }
    }
}