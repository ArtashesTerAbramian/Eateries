using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eateries.Infrastructure.Persistence.Repositories;

public class MenuDishRepositoryAsync : GenericRepositoryAsync<MenuDish>, IMenuDishRepositoryAsync
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDataShapeHelper<MenuDish> _shapeHelper;
    private readonly DbSet<MenuDish> _menuDish;

    public MenuDishRepositoryAsync(
        ApplicationDbContext dbContext,
        IDataShapeHelper<MenuDish> shapeHelper,
        ILogger<GenericRepositoryAsync<MenuDish>> logger)
                            : base(dbContext, logger)
    {
        _dbContext = dbContext;
        _menuDish = dbContext.Set<MenuDish>();
        _shapeHelper = shapeHelper;
    }
    
    public async Task<List<MenuDish>> GetMenuDishesByMenuIdAsync(Guid menuId)
    {
        return await _dbContext.MenuDishes
            .Where(s => s.MenuId == menuId)
            .ToListAsync();
    }
    
    public async Task<List<Dish>> GetAllDishesForMenu(Guid menuId)
    {
        return await _dbContext.MenuDishes
            .Where(md => md.MenuId == menuId)
            .Select(md => md.Dish)
            .ToListAsync();

    }
}