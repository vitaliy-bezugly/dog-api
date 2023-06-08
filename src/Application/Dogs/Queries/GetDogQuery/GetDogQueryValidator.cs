using FluentValidation;

namespace Application.Dogs.Queries.GetDogQuery;

public class GetDogQueryValidator : AbstractValidator<GetDogQuery>
{
    public GetDogQueryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(256);
    }
}