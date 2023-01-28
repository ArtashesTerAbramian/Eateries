using Eateries.Application.Features.Addresses.Queries.GetAddresses;
using Eateries.Application.Parameters;
using Eateries.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Application.Interfaces.Repositories
{
    public interface IAddressRepositoryAsync : IGenericRepositoryAsync<Address>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedAddressReponseAsync(GetAddressQuery requestParameter);
    }
}
