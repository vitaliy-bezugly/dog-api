using FluentValidation;

namespace Application.Dogs.Queries.GetDogsQuery;

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
        
        RuleFor(x => x.Attribute)
            .Must(x => string.IsNullOrEmpty(x) || x == "name" || x == "color" || x == "tail_length" || x == "weight")
            .WithMessage("Attribute must be null or one of the following: name, color, tailLength, weight.");
        
        RuleFor(x => x.Order)
            .Must(x => string.IsNullOrEmpty(x) || x == "asc" || x == "desc")
            .WithMessage("Order must be null or one of the following: asc, desc.");
    }
}