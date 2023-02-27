using AutoMapper;
using Eateries.Application.DTOs;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using Eateries.Domain.Enums;
using MediatR;

namespace Eateries.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Response<Guid>>
{
    public Guid UserId { get; set; }
    public Guid EateryId { get; set; }
    public List<OrderDishDto> OrderDishes { get; set; }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<Guid>>
{
    private readonly IOrderRepositoryAsync _orderRepositoryAsync;
    private readonly IDishRepositoryAsync _dishRepositoryAsync;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(
        IOrderRepositoryAsync orderRepositoryAsync,
        IDishRepositoryAsync dishRepositoryAsync,
        IMapper mapper)
    {
        _orderRepositoryAsync = orderRepositoryAsync;
        _dishRepositoryAsync = dishRepositoryAsync;
        _mapper = mapper;
    }

    public async Task<Response<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            UserId = request.UserId,
            EateryId = request.EateryId,
            OrderDate = DateTime.UtcNow,
            TotalCost = 0, // will be updated when dishes are added
            Status = OrderStatus.Pending,
            OrderHistories = new List<OrderHistory>()
        };

        foreach (var orderDishDto in request.OrderDishes)
        {
            var dish = await _dishRepositoryAsync.GetByIdAsync(orderDishDto.DishId);
            if (dish == null)
            {
                throw new Exception($"Dish with id {orderDishDto.DishId} not found");
            }

            var orderDish = new OrderDish
            {
                DishId = orderDishDto.DishId,
                Quantity = orderDishDto.Quantity,
                Order = order
            };

            order.OrderDishes.Add(orderDish);

            order.TotalCost += orderDish.Quantity * dish.Price;
        }

        await _orderRepositoryAsync.AddAsync(order);

        return new Response<Guid>(order.Id);
    }
}