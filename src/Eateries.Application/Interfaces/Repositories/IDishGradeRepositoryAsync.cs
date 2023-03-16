using Eateries.Domain.Entities;

namespace Eateries.Application.Interfaces.Repositories;

public interface IDishGradeRepositoryAsync : IGenericRepositoryAsync<DishGrade>
{
    Task<DishGrade> GetGradeByIds(Guid menuId, Guid dishId);
}