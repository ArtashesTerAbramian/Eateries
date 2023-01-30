using Eateries.Application.Helpers;
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
    internal class MenuRepositoryAsync : GenericRepositoryAsync<Menu>, IMenuRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDataShapeHelper<Menu> _dataShapeHelper;
        private readonly DbSet<Menu> _menuSet;

        public MenuRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Menu> dataShapeHelper) 
            : base(dbContext)
        {
            this._dbContext = dbContext;
            this._dataShapeHelper = dataShapeHelper;
            this._menuSet = dbContext.Set<Menu>();
        }
    }
}
