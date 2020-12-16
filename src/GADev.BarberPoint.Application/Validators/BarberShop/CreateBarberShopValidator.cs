using FluentValidation;
using GADev.BarberPoint.Application.Commands.BarberShop;

namespace GADev.BarberPoint.Application.Validators.BarberShop
{
    public class CreateBarberShopValidator : AbstractValidator<CreateBarberShopCommand>
    {
        public CreateBarberShopValidator()
        {
            RuleFor(a => a.AdminUserId)
                .NotNull().WithMessage("O campo 'AdminUserId' não pode ser nulo")
                .GreaterThan(0).WithMessage("O campo 'AdminUserId' deve ser maior que 0");
                
            RuleFor(a => a.Name)
                .NotNull().WithMessage("O campo 'Name' é obrigatório")
                .NotEmpty().WithMessage("O campo 'Name' não pode estar vazio")
                .MinimumLength(6).WithMessage("O campo 'Name' deve conter pelo menos 6 caracteres")
                .MaximumLength(30).WithMessage("O campo 'Name' deve conter no máximo 30 caracteres");

            RuleFor(a => a.PublicPlace)
                .NotNull().WithMessage("O campo 'PublicPlace' é obrigatório")
                .NotEmpty().WithMessage("O campo 'PublicPlace' não pode estar vazio")
                .MinimumLength(6).WithMessage("O campo 'PublicPlace' deve conter pelo menos 6 caracteres")
                .MaximumLength(80).WithMessage("O campo 'PublicPlace' deve conter no máximo 80 caracteres");

            RuleFor(a => a.Neighborhood)
                .NotNull().WithMessage("O campo 'Neighborhood' é obrigatório")
                .NotEmpty().WithMessage("O campo 'Neighborhood' não pode estar vazio")
                .MinimumLength(6).WithMessage("O campo 'Neighborhood' deve conter pelo menos 6 caracteres")
                .MaximumLength(50).WithMessage("O campo 'Neighborhood' deve conter no máximo 50 caracteres");

            RuleFor(a => a.Locality)
                .NotNull().WithMessage("O cmapo 'Locality' é obrigatório")
                .NotEmpty().WithMessage("O campo 'Locality' não pode estar vazio")
                .MinimumLength(6).WithMessage("O campo 'Locality' deve conter pelo menos 6 caracteres")
                .MaximumLength(70).WithMessage("O campo 'Locality' deve conter no máximo 70 caracteres");

            RuleFor(a => a.State)
                .NotNull().WithMessage("O cmapo 'State' é obrigatório")
                .NotEmpty().WithMessage("O campo 'State' não pode estar vazio")
                .MinimumLength(2).WithMessage("O campo 'State' deve conter pelo menos 2 caracteres")
                .MaximumLength(2).WithMessage("O campo 'State' deve conter no máximo 2 caracteres");

            RuleFor(a => a.Latitude)
                .NotNull().WithMessage("O cmapo 'Latitude' é obrigatório");

            RuleFor(a => a.Longitude)
                .NotNull().WithMessage("O cmapo 'Longitude' é obrigatório");
        }
    }
}