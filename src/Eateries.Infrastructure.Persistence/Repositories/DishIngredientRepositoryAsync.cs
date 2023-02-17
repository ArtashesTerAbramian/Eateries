using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eateries.Infrastructure.Persistence.Repositories;

public class DishIngredientRepositoryAsync : GenericRepositoryAsync<DishIngredients>, IDishIngredientRepositoryAsync
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDataShapeHelper<DishIngredients> _dataShapeHelper;
    private readonly DbSet<DishIngredients> _dishIngredient;

    public DishIngredientRepositoryAsync(
        ApplicationDbContext dbContext,
        IDataShapeHelper<DishIngredients> dataShapeHelper) : base(dbContext)
    {
        _dbContext = dbContext;
        _dishIngredient = dbContext.Set<DishIngredients>();
        _dataShapeHelper = dataShapeHelper;
    }
}