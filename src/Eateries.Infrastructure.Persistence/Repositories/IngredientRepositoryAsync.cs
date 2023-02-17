using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eateries.Infrastructure.Persistence.Repositories;

public class IngredientRepositoryAsync : GenericRepositoryAsync<Ingredient>, IIngredientRepositoryAsync
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDataShapeHelper<Ingredient> _dataShapeHelper;
    private readonly DbSet<Ingredient> _ingredient;

    public IngredientRepositoryAsync(
        ApplicationDbContext dbContext,
        IDataShapeHelper<Ingredient> dataShapeHelper) : base(dbContext)
    {
        _dbContext = dbContext;
        _ingredient = dbContext.Set<Ingredient>();
        _dataShapeHelper = dataShapeHelper;
    }
}