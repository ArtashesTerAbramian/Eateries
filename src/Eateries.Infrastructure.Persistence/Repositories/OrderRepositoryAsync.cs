using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eateries.Infrastructure.Persistence.Repositories;

public class OrderRepositoryAsync : GenericRepositoryAsync<Order>, IOrderRepositoryAsync
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDataShapeHelper<Order> _dataShapeHelper;
    private readonly ILogger<GenericRepositoryAsync<Order>> _logger;
    private readonly DbSet<Order> _orders;

    public OrderRepositoryAsync(
        ApplicationDbContext dbContext,
        IDataShapeHelper<Order> dataShapeHelper,
        ILogger<GenericRepositoryAsync<Order>> logger)
        : base(dbContext, logger)
    {
        _dbContext = dbContext;
        _orders = dbContext.Set<Order>();
        _dataShapeHelper = dataShapeHelper;
        _logger = logger;
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId)
    {
        return await _orders
            .Where(o => o.UserId == userId)
            .Include(i => i.OrderDishes)
            .ThenInclude(i => i.Dish)
            .ToListAsync();

        /*var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
        };

        // Serialize the orders to JSON using the configured options
        var json = JsonSerializer.Serialize(orders, options);

        // Deserialize the JSON back to a List<Order> using the configured options
        return JsonSerializer.Deserialize<List<Order>>(json, options);*/
    }
}