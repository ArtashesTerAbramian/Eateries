using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Infrastructure.Persistence.Repositories
{
    internal class EateryRepositoryAsync : GenericRepositoryAsync<Eatery>, IEateryRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDataShapeHelper<Eatery> _dataShapeHelper;
        private readonly DbSet<Eatery> eateries;

        public EateryRepositoryAsync(ApplicationDbContext dbContext, 
            IDataShapeHelper<Eatery> dataShapeHelper) 
            : base(dbContext)
        {
            this._dbContext = dbContext;
            this._dataShapeHelper = dataShapeHelper;
            this.eateries = dbContext.Set<Eatery>();
        }
    }
}
