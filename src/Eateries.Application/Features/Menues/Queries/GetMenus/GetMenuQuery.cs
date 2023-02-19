using AutoMapper;
using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Parameters;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Menues.Queries.GetMenus
{
    public class GetMenuQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? EateryId { get; set; }
    }

    public class GetAllMenuQueryHandler : IRequestHandler<GetMenuQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetAllMenuQueryHandler(
            IMenuRepositoryAsync menuRepositoryAsync,
            IMapper mapper,
            IModelHelper modelHelper)
        {
            this._menuRepositoryAsync = menuRepositoryAsync;
            this._mapper = mapper;
            this._modelHelper = modelHelper;
        }


        async Task<PagedResponse<IEnumerable<Entity>>> IRequestHandler<GetMenuQuery, PagedResponse<IEnumerable<Entity>>>.Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetMenuViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetMenuViewModel>();
            }
            // query based on filter
            var entityPositions = await _menuRepositoryAsync.GetPagedMenuesReponseAsync(validFilter);
            var data = entityPositions.data;
            RecordsCount recordCount = entityPositions.recordsCount;

            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}
