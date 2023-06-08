using FluentValidation;

namespace Application.Dogs.Queries;

public class GetDogsQueryValidator : AbstractValidator<GetDogsQuery>
{
    public GetDogsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber must be greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageSize must be greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(50)
            .WithMessage("PageSize must be less than or equal to 50.");
    }
}