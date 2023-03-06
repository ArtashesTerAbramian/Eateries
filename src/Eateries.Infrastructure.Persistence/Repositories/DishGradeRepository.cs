using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using Microsoft.Extensions.Logging;

namespace Eateries.Infrastructure.Persistence.Repositories;

public class DishGradeRepository : GenericRepositoryAsync<DishGrade>,IDishGradeRepository
{
    public DishGradeRepository(ApplicationDbContext dbContext,  
        ILogger<GenericRepositoryAsync<DishGrade>> logger) : base(dbContext, logger)
    {
    }
}