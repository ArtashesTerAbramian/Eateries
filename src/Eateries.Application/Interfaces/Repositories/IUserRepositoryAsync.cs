using Eateries.Application.Features.User.Queries.GetUsers;
using Eateries.Application.Parameters;
using Eateries.Domain.Entities;

namespace Eateries.Application.Interfaces.Repositories;

public interface IUserRepositoryAsync : IGenericRepositoryAsync<User>
{
    Task<User> GetUserByEmailAsync(string email);
    Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedUsersReponseAsync(GetUsersQuery requestParameter);

}