using Eateries.Domain.Entities;

namespace Eateries.Application.Interfaces;

public interface IMarkovChainFoodSuggestorService
{
    Task<List<Dish>> SuggestFood(Guid userId, Guid menuId, int numSuggestions);
}