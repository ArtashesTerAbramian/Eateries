using Eateries.Application.Features.Eateries.Queries.GetEateries;
using Eateries.Application.Parameters;
using Eateries.Domain.Entities;

namespace Eateries.Application.Interfaces.Repositories
{
    public interface IEateryRepositoryAsync : IGenericRepositoryAsync<Eatery>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedEateriesReponseAsync(GetEateriesQuery requestParameter);

    }
}
