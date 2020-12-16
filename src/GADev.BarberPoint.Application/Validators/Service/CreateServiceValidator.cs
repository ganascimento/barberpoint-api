using FluentValidation;
using GADev.BarberPoint.Application.Commands.Service;

namespace GADev.BarberPoint.Application.Validators.Service
{
    public class CreateServiceValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceValidator()
        {
            RuleFor(a => a.BarberShopId)
                .NotNull().WithMessage("O campo 'BarberShopId' é obrigatório");   

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