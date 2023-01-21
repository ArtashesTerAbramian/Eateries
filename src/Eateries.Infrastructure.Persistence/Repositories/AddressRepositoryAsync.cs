using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Infrastructure.Persistence.Repositories
{
    public class AddressRepositoryAsync : GenericRepositoryAsync<Address>, IAddressRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public AddressRepositoryAsync(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            this._dbContext = dbContext;
        }
    }
}
