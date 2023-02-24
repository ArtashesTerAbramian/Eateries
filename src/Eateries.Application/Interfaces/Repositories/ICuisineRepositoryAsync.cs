using Eateries.Application.Features.Cuisines.Queries.GetCuisines;
using Eateries.Application.Parameters;
using Eateries.Domain.Entities;

namespace Eateries.Application.Interfaces.Repositories;

public interface ICuisineRepositoryAsync : IGenericRepositoryAsync<Cuisine>
{
    Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedCuisinesReponseAsync(GetCuisinesQuery requestParameter);
}