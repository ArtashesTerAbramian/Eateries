using Eateries.Domain.Entities;

namespace Eateries.Application.Interfaces;

public interface IGenerateDishSuggestionAsync
{
    Task<List<Dish>> GenerateDishSuggestions(Guid userId, int numSuggestions);
}