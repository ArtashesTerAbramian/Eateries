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
    public class EateryRepositoryAsync : GenericRepositoryAsync<Eatery>, IEateryRepositoryAsync
    {
        public EateryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
