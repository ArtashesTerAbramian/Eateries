using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Parameters;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Dishes.Queries.GetDishes;

public class GetDishesQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
{
    public string? Name { get; set; }
    public Guid CuisieneId { get; set; }
}

public class GetDishesQueryHandler : IRequestHandler<GetDishesQuery, PagedResponse<IEnumerable<Entity>>>
{
    private readonly IDishRepositoryAsync _dishRepositoryAsync;
    private readonly IModelHelper _modelHelper;

    public GetDishesQueryHandler(IDishRepositoryAsync dishRepositoryAsync, IModelHelper modelHelper)
    {
        _dishRepositoryAsync = dishRepositoryAsync;
        _modelHelper = modelHelper;
    }

    public async Task<PagedResponse<IEnumerable<Entity>>> Handle(
        GetDishesQuery request,
        CancellationToken cancellationToken)
    {
        var validFilter = request;

        if (!string.IsNullOrEmpty(validFilter.Fields))
        {
            validFilter.Fields = _modelHelper.ValidateModelFields<GetDishesViewModel>(validFilter.Fields);
        }

        if (string.IsNullOrEmpty(validFilter.Fields))
        {
            validFilter.Fields = _modelHelper.GetModelFields<GetDishesViewModel>();
        }

        var dishEntities = await _dishRepositoryAsync.GetPagedAddressReponseAsync(request);
        RecordsCount  recordCount = dishEntities.recordsCount;
        var data = dishEntities.data;
        return new
            PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
    }
}