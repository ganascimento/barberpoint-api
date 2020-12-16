using FluentValidation;
using GADev.BarberPoint.Application.Commands.Service;

namespace GADev.BarberPoint.Application.Validators.Service
{
    public class ChangeServiceValidator : AbstractValidator<ChangeServiceCommand>
    {
        public ChangeServiceValidator()
        {
            RuleFor(a => a.Id)
                .NotNull().WithMessage("O 'Id' não pode ser nulo")
                .GreaterThan(0).WithMessage("O campo 'Id' deve ser maior que 0");

            RuleFor(a => a.BarberShopId)
                .NotNull().WithMessage("O campo 'BarberShopId' é obrigatório")
                .GreaterThan(0).WithMessage("O campo 'BarberShopId' deve ser maior que 0");

            RuleFor(a => a.Name)
                .NotNull().WithMessage("O campo 'Name' é obrigatório")
                .NotEmpty().WithMessage("O campo 'Name' não pode estar vazio")
                .MinimumLength(6).WithMessage("O campo 'Name' deve conter pelo menos 6 caracteres")
                .MaximumLength(45).WithMessage("O campo 'Name' deve conter no máximo 45 caracteres");

            RuleFor(a => a.Duration)
                .NotNull().WithMessage("O campo 'Duration' é obrigatório");

            RuleFor(a => a.Value)
                .NotNull().WithMessage("O campo 'Value' é obrigatório")
                .GreaterThan(0).WithMessage("O campo 'Value' deve ser maior que 0");
        }
    }
}