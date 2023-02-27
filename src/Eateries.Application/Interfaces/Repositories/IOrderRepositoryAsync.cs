using Eateries.Application.Features.Orders.Commands.CreateOrder;
using Eateries.Domain.Entities;

namespace Eateries.Application.Interfaces.Repositories;

public interface IOrderRepositoryAsync : IGenericRepositoryAsync<Order>
{
    
}