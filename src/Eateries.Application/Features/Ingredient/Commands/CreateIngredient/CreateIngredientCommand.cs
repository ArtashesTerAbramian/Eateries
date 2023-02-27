using AutoMapper;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using MediatR;

namespace Eateries.Application.Features.Ingredient.Commands.CreateIngredient;

public class CreateIngredientCommand : IRequest<Response<Guid>>
{
    public string Name { get; set; }
}

public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, Response<Guid>>
{
    private readonly IIngredientRepositoryAsync _ingredientRepositoryAsync;
    private readonly IMapper _mapper;

    public CreateIngredientCommandHandler(IIngredientRepositoryAsync ingredientRepositoryAsync, IMapper mapper)
    {
        _ingredientRepositoryAsync = ingredientRepositoryAsync;
        _mapper = mapper;
    }

    public async Task<Response<Guid>> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        var ingredient = new Domain.Entities.Ingredient { IngredientName = request.Name };
        await _ingredientRepositoryAsync.AddAsync(ingredient);
        return new Response<Guid>(ingredient.Id);
    }
}