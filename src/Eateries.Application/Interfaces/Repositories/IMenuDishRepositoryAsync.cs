using Eateries.Domain.Entities;

namespace Eateries.Application.Interfaces.Repositories;

public interface IMenuDishRepositoryAsync : IGenericRepositoryAsync<MenuDish>
{
    Task<List<Dish>> GetAllDishesForMenu(Guid menuId);
}