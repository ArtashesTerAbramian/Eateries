using Eateries.Application.Features.Dishes.Queries.GetDishes;
using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Parameters;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace Eateries.Infrastructure.Persistence.Repositories;

public class DishRepositoryAsync : GenericRepositoryAsync<Dish>, IDishRepositoryAsync
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDataShapeHelper<Dish> _dataShaper;
    private readonly DbSet<Dish> _dishes;
    public DishRepositoryAsync(
        ApplicationDbContext dbContext,
        IDataShapeHelper<Dish> dataShaper,
        ILogger<GenericRepositoryAsync<Dish>> logger)
        : base(dbContext, logger)
    {
        _dbContext = dbContext;
        _dishes = _dbContext.Set<Dish>();
        _dataShaper = dataShaper;
    }

    public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedDishesReponseAsync(
        GetDishesQuery requestParameter)
    {
        var dishName = requestParameter.Name;
        var dishCuisineId = requestParameter.CuisieneId;

        var pageNumber = requestParameter.PageNumber;
        var pageSize = requestParameter.PageSize;
        var orderBy = requestParameter.OrderBy;
        var fields = requestParameter.Fields;

        int recordsTotal, recordsFiltered;

        // Setup IQueryable
        var result = _dishes
            .AsNoTracking();

        // Count records total
        recordsTotal = await result.CountAsync();

        // filter data
        FilterByColumn(ref result, dishName, dishCuisineId);

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
            result = result.Select<Dish>("new(" + fields + ")");
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

    private void FilterByColumn(
        ref IQueryable<Dish> dishes,
        string dishName,
        Guid dishCuisineId)
    {
        if (!dishes.Any())
            return;

        if (dishCuisineId == Guid.Empty && string.IsNullOrEmpty(dishName))
            return;

        var predicate = PredicateBuilder.New<Dish>();

        if (!string.IsNullOrEmpty(dishName))
            predicate = predicate.Or(p => p.Name.Contains(dishName.Trim()));

        if (dishCuisineId != Guid.Empty)
            predicate = predicate.Or(p => p.CuisineId == dishCuisineId);

        dishes = dishes.Where(predicate);
    }
}