using FluentValidation;
using GADev.BarberPoint.Application.Commands.Barber;

namespace GADev.BarberPoint.Application.Validators.Barber
{
    public class CreateBarberValidator : AbstractValidator<CreateBarberCommand>
    {
        public CreateBarberValidator()
        {
            RuleFor(a => a.Name)
                .NotNull().WithMessage("O campo 'Name' é obrigatório")
                .NotEmpty().WithMessage("O campo 'Name' não pode estar vazio")
                .MinimumLength(6).WithMessage("O campo 'Name' deve conter pelo menos 6 caracteres")
                .MaximumLength(50).WithMessage("O campo 'Name' deve conter no máximo 50 caracteres");

            RuleFor(a => a.TimeFinishWork)
                .NotNull().WithMessage("O 'TimeFinishWork' não pode ser nulo");

            RuleFor(a => a.TimeStartWork)
                .NotNull().WithMessage("O 'TimeStartWork' não pode ser nulo");

            RuleFor(a => a.UserId)
                .NotNull().WithMessage("O campo 'UserId' é obrigatório");

            RuleFor(a => a.BarberShopId)
                .NotNull().WithMessage("O campo 'BarberShopId' é obrigatório")
                .GreaterThan(0).WithMessage("O campo 'BarberShopId' deve ser maior que 0");
        }
    }
}