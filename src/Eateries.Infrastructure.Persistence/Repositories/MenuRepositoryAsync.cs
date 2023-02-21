using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Parameters;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Eateries.Application.Features.Menues.Queries.GetMenus;
using Microsoft.Extensions.Logging;

namespace Eateries.Infrastructure.Persistence.Repositories
{
    internal class MenuRepositoryAsync : GenericRepositoryAsync<Menu>, IMenuRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDataShapeHelper<Menu> _dataShaper;
        private readonly ILogger<MenuRepositoryAsync> _logger;
        private readonly DbSet<Menu> _menu;

        public MenuRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Menu> dataShapeHelper,
            ILogger<MenuRepositoryAsync> logger) 
            : base(dbContext, logger)
        {
            this._dbContext = dbContext;
            this._dataShaper = dataShapeHelper;
            _logger = logger;
            this._menu = dbContext.Set<Menu>();
        }

        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)>
                        GetPagedMenuesReponseAsync(GetMenuQuery requestParameter)
        {
            var menuName = requestParameter.Name;
            var menuDescription = requestParameter.Description;
    
            var pageNumber = requestParameter.PageNumber;
            var pageSize = requestParameter.PageSize;
            var orderBy = requestParameter.OrderBy;
            var fields = requestParameter.Fields;

            int recordsTotal, recordsFiltered;

            // Setup IQueryable
            var result = _menu
                .AsNoTracking();

            // Count records total
            recordsTotal = await result.CountAsync();

            // filter data
            FilterByColumn(ref result, menuName, menuDescription);

            // Count records after filter
            recordsFiltered = await result.CountAsync();

            //set Record counts
            var recordsCount = new RecordsCount
            {
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };

            // set order by
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                result = result.OrderBy(orderBy);
            }

            // select columns
            if (!string.IsNullOrWhiteSpace(fields))
            {
                result = result.Select<Menu>("new(" + fields + ")");
            }
            // paging
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // retrieve data to list
            var resultData = await result.ToListAsync();
            // shape data
            var shapeData = _dataShaper.ShapeData(resultData, fields);

            return (shapeData, recordsCount);
        }

        private void FilterByColumn(ref IQueryable<Menu> menus, string menusName, string menusDescription)
        {
            if (!menus.Any())
                return;

            if (string.IsNullOrEmpty(menusDescription) && string.IsNullOrEmpty(menusName))
                return;

            var predicate = PredicateBuilder.New<Menu>();

            if (!string.IsNullOrEmpty(menusName))
                predicate = predicate.Or(p => p.Name.Contains(menusName.Trim()));

            if (!string.IsNullOrEmpty(menusDescription))
                predicate = predicate.Or(p => p.Description.Contains(menusDescription.Trim()));

            menus = menus.Where(predicate);
        }
    }
}
