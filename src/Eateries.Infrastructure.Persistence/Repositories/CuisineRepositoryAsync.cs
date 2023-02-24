using System.Linq.Dynamic.Core;
using Eateries.Application.Features.Cuisines.Queries.GetCuisines;
using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Parameters;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eateries.Infrastructure.Persistence.Repositories;

public class CuisineRepositoryAsync : GenericRepositoryAsync<Cuisine>, ICuisineRepositoryAsync
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDataShapeHelper<Cuisine> _dataShapeHelper;
    private readonly DbSet<Cuisine> _cuisine;

    public CuisineRepositoryAsync(
        ApplicationDbContext dbContext, 
        IDataShapeHelper<Cuisine> dataShapeHelper,
        ILogger<GenericRepositoryAsync<Cuisine>> logger) 
        : base(dbContext, logger)
    {
        this._dbContext = dbContext;
        this._cuisine = dbContext.Cuisines;
        this._dataShapeHelper = dataShapeHelper;
    }

      public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedCuisinesReponseAsync(
        GetCuisinesQuery requestParameter)
    {
        var cuisineName = requestParameter.Name;
        
        var pageNumber = requestParameter.PageNumber;
        var pageSize = requestParameter.PageSize;
        var orderBy = requestParameter.OrderBy;
        var fields = requestParameter.Fields;

        int recordsTotal, recordsFiltered;

        // Setup IQueryable
        var result = _cuisine
            .AsNoTracking();

        // Count records total
        recordsTotal = await result.CountAsync();

        // filter data
        FilterByColumn(ref result, cuisineName);

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
            result = result.Select<Cuisine>("new(" + fields + ")");
        }

        // paging
        result = result
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        // retrieve data to list
        var resultData = await result.ToListAsync();
        // shape data
        var shapeData = _dataShapeHelper.ShapeData(resultData, fields);

        return (shapeData, recordsCount);
    }

    private void FilterByColumn(ref IQueryable<Cuisine> cuisines, string cuisineName)
        {
            if (!cuisines.Any())
                return;

            if (string.IsNullOrEmpty(cuisineName))
                return;

            var predicate = PredicateBuilder.New<Cuisine>();

            if (!string.IsNullOrEmpty(cuisineName))
                predicate = predicate.Or(p => p.Name.Contains(cuisineName.Trim()));

            cuisines = cuisines.Where(predicate);
        }
}