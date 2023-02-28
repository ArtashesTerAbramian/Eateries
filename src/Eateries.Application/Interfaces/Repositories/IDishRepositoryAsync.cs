using Eateries.Application.Features.Dishes.Queries.GetDishes;
using Eateries.Application.Parameters;
using Eateries.Domain.Entities;

namespace Eateries.Application.Interfaces.Repositories;

public interface IDishRepositoryAsync : IGenericRepositoryAsync<Dish>
{
    Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedAddressReponseAsync(GetDishesQuery requestParameter);
}