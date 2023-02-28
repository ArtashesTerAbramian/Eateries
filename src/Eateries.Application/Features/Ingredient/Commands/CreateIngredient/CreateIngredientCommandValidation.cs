using FluentValidation;

namespace Eateries.Application.Features.Ingredient.Commands.CreateIngredient;

public class CreateIngredientCommandValidation : AbstractValidator<CreateIngredientCommand>
{
    public CreateIngredientCommandValidation()
    {
        
    }
}