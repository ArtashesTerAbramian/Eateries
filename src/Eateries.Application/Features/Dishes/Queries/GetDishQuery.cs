using AutoMapper;
using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Parameters;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Menues.Queries
{
    public class GetDishQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class GetAllDishQueryHandler : IRequestHandler<GetDishQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IDishRepositoryAsync _dishRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetAllDishQueryHandler(
            IDishRepositoryAsync dishRepositoryAsync,
            IMapper mapper,
            IModelHelper modelHelper)
        {
            this._dishRepositoryAsync = dishRepositoryAsync;
            this._mapper = mapper;
            this._modelHelper = modelHelper;
        }


        async Task<PagedResponse<IEnumerable<Entity>>> IRequestHandler<GetDishQuery, PagedResponse<IEnumerable<Entity>>>.Handle(GetDishQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetDishViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetDishViewModel>();
            }
            // query based on filter
            var entityPositions = await _dishRepositoryAsync.GetPagedDishesReponseAsync(validFilter);
            var data = entityPositions.data;
            RecordsCount recordCount = entityPositions.recordsCount;

            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}
