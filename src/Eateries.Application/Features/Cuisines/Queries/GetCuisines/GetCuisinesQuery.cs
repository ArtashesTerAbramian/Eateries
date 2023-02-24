using AutoMapper;
using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Parameters;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Cuisines.Queries.GetCuisines;

public class GetCuisinesQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
{
    public string? Name { get; set; }
}

internal class GetCuisineQueryQueryHandler : IRequestHandler<GetCuisinesQuery, PagedResponse<IEnumerable<Entity>>>
{
    private readonly ICuisineRepositoryAsync _cuisineRepositoryAsync;
    private readonly IMapper _mapper;
    private readonly IModelHelper _modelHelper;

    public GetCuisineQueryQueryHandler(
        ICuisineRepositoryAsync cuisineRepositoryAsync,
        IMapper mapper,
        IModelHelper modelHelper)
    {
        _cuisineRepositoryAsync = cuisineRepositoryAsync;
        _mapper = mapper;
        _modelHelper = modelHelper;
    }
    
    public async Task<PagedResponse<IEnumerable<Entity>>> Handle(
        GetCuisinesQuery request, 
        CancellationToken cancellationToken)
    {
        var validFilter = request;
        
        if (!string.IsNullOrEmpty(validFilter.Fields))
        {
            validFilter.Fields = _modelHelper.ValidateModelFields<GetCuisineViewModel>(validFilter.Fields);
        }
        if (string.IsNullOrEmpty(validFilter.Fields))
        {
            validFilter.Fields = _modelHelper.GetModelFields<GetCuisineViewModel>();
        }

        var entityCuisine = await _cuisineRepositoryAsync.GetPagedCuisinesReponseAsync(validFilter);
        var data = entityCuisine.data;
        var recordsCount = entityCuisine.recordsCount;

        return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordsCount);
    }
}