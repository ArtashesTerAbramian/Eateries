using Eateries.Application.Features.Menues.Queries;
using Eateries.Application.Parameters;
using Eateries.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Application.Interfaces.Repositories
{
    public interface IMenuRepositoryAsync : IGenericRepositoryAsync<Menu>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedMenuesReponseAsync(GetMenuQuery requestParameter);
    }
}
