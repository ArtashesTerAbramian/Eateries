using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using Microsoft.Extensions.Logging;

namespace Eateries.Infrastructure.Persistence.Repositories;

public class DishRepositoryAsync : GenericRepositoryAsync<Dish>, IDishRepositoryAsync
{
    public DishRepositoryAsync(
        ApplicationDbContext dbContext, 
        ILogger<GenericRepositoryAsync<Dish>> logger) 
        : base(dbContext, logger)
    {
    }
}