using AutoMapper;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using MediatR;
using Eateries.Domain.Entities;

namespace Eateries.Application.Features.Cuisine.Command.CreateCuisine;

public class CreateCuisineCommand : IRequest<Response<Guid>>
{
    public string Name { get; set; }
}

public class CreateCuisineCommandHandler : IRequestHandler<CreateCuisineCommand, Response<Guid>>
{
    private readonly ICuisineRepositoryAsync _cuisineRepositoryAsync;
    private readonly IMapper _mapper;

    public CreateCuisineCommandHandler(
        ICuisineRepositoryAsync cuisineRepositoryAsync,
        IMapper mapper)
    {
        _cuisineRepositoryAsync = cuisineRepositoryAsync;
        _mapper = mapper;
    }
    public async Task<Response<Guid>> Handle(CreateCuisineCommand request, CancellationToken cancellationToken)
    {
        
        var cuisine = _mapper.Map<Domain.Entities.Cuisine>(request);
        await _cuisineRepositoryAsync.AddAsync(cuisine);
        return new Response<Guid>(cuisine.Id);

    }
}