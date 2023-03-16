using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eateries.Infrastructure.Persistence.Repositories;

public class DishGradeRepositoryAsync : GenericRepositoryAsync<DishGrade>,IDishGradeRepositoryAsync
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<DishGrade> _dishGrades;

    public DishGradeRepositoryAsync(ApplicationDbContext dbContext,  
        ILogger<GenericRepositoryAsync<DishGrade>> logger) : base(dbContext, logger)
    {
        _dbContext = dbContext;
        _dishGrades = dbContext.Set<DishGrade>();
    }

    public async Task<DishGrade> GetGradeByIds(Guid menuId, Guid dishId)
    {
        return await _dishGrades
            .FirstOrDefaultAsync(d => d.DishId == dishId && d.MenuId == menuId);
    }
}