using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eateries.Infrastructure.Persistence.Repositories;

public class CuisineRepositoryAsync : GenericRepositoryAsync<Cuisine>, ICuisineRepositoryAsync
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDataShapeHelper<Cuisine> _dataShapeHelper;
    private readonly DbSet<Cuisine> _cuisine;

    public CuisineRepositoryAsync(
        ApplicationDbContext dbContext,
        IDataShapeHelper<Cuisine> dataShapeHelper) : base(dbContext)
    {
        _dbContext = dbContext;
        _cuisine = dbContext.Set<Cuisine>();
        _dataShapeHelper = dataShapeHelper;
    }
}