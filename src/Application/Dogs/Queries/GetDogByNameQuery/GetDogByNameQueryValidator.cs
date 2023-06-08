using FluentValidation;

namespace Application.Dogs.Queries.GetDogByNameQuery;

public class GetDogByNameQueryValidator : AbstractValidator<GetDogByNameQuery>
{
    public GetDogByNameQueryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(256);
    }
}