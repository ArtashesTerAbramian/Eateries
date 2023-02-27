using Eateries.Domain.Entities;

namespace Eateries.Application.Interfaces.Repositories;

public interface IOrderRepositoryAsync : IGenericRepositoryAsync<Order>
{
    Task<Guid> AddAnOrderAsync(Order order, List<Dish> dishes);
}