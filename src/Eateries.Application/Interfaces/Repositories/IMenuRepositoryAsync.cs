using Eateries.Application.Parameters;
using Eateries.Domain.Entities;
using Eateries.Application.Features.Menues.Queries.GetMenus;

namespace Eateries.Application.Interfaces.Repositories
{
    public interface IMenuRepositoryAsync : IGenericRepositoryAsync<Menu>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedMenuesReponseAsync(GetMenuQuery requestParameter);
        Task<Menu> GetMenuById(Guid menuId);
    }
}
