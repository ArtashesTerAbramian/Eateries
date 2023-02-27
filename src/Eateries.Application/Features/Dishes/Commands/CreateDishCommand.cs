using AutoMapper;
using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Dishes.Commands;

public class CreateDishCommand : IRequest<Response<Guid>>
{
    public string Name { get; set; }

    public int TimeMins { get; set; }

    public Guid CuisineId { get; set; }

    public string Instructions { get; set; }

    public string Description { get; set; }

    public string Note { get; set; }

    public decimal Price { get; set; }

    public string ImageUrl { get; set; }
    
    public List<Guid> IngredientIds { get; set; }
}

public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, Response<Guid>>
{
    private readonly IDishRepositoryAsync _dishRepositoryAsync;
    private readonly IIngredientRepositoryAsync _ingredientRepositoryAsync;
    private readonly IMapper _mapper;

    public CreateDishCommandHandler(IDishRepositoryAsync dishRepositoryAsync,
        IIngredientRepositoryAsync ingredientRepositoryAsync,
        IMapper mapper)
    {
        _dishRepositoryAsync = dishRepositoryAsync;
        _ingredientRepositoryAsync = ingredientRepositoryAsync;
        _mapper = mapper;
    }

    public async Task<Response<Guid>> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        var dish = _mapper.Map<Dish>(request);
        dish.DishIngredients = new List<DishIngredient>();
        foreach (var ingredientId in request.IngredientIds)
        {
            var ingredient = await _ingredientRepositoryAsync.GetByIdAsync(ingredientId);
            if (ingredient == null)
                throw new ApiException($"Ingredient with id {ingredient} not found");
            var dishIngredients = new DishIngredient
            {
                IngredientId = ingredientId,
                Dish = dish
            };
            dish.DishIngredients.Add(dishIngredients);
        }
        
        await _dishRepositoryAsync.AddAsync(dish);
        
        return new Response<Guid>(dish.Id);
    }
}