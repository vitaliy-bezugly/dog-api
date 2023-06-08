using FluentValidation;

namespace Application.Dogs.Commands.CreateDogCommand;

public class CreateDogCommandValidator : AbstractValidator<CreateDogCommand>
{
    public CreateDogCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(256);
        
        RuleFor(x => x.Color)
            .NotEmpty()
            .MaximumLength(512);
        
        RuleFor(x => x.TailLength)
            .GreaterThan(1);
        
        RuleFor(x => x.Weight)
            .GreaterThan(1);
    }
}