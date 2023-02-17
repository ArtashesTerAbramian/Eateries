using Eateries.Application.Features.Cuisine.Command.CreateCuisine;
using Eateries.Application.Interfaces.Repositories;
using FluentValidation;

namespace Eateries.Application.Features.Cuisine.Command.CreateCuisine;

public class CreateCuisineCommandValidator : AbstractValidator<CreateCuisineCommand>
{
    private readonly ICuisineRepositoryAsync _cuisineRepositoryAsync;

    public CreateCuisineCommandValidator(ICuisineRepositoryAsync cuisineRepositoryAsync)
    {
        _cuisineRepositoryAsync = cuisineRepositoryAsync;

        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("{PropertyName} can't be empty");
    }
}