using AutoMapper;
using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Parameters;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Eateries.Queries.GetEateries
{
    public class GetEateriesQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public string? Name { get; set; }
        public string? EateryType { get; set; }
    }

    public class GetEateriesQueryHandler : IRequestHandler<GetEateriesQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IEateryRepositoryAsync _eateryRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetEateriesQueryHandler(
            IEateryRepositoryAsync eateryRepositoryAsync, 
            IMapper mapper,
            IModelHelper modelHelper)
        {
            this._eateryRepositoryAsync = eateryRepositoryAsync;
            this._mapper = mapper;
            this._modelHelper = modelHelper;
        }
        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetEateriesQuery request, CancellationToken cancellationToken)
        {

            var validFilter = request;
            
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                
                validFilter.Fields = _modelHelper.ValidateModelFields<GetEateriesViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                validFilter.Fields = _modelHelper.GetModelFields<GetEateriesViewModel>();
            }
            
            var entityEateries = await _eateryRepositoryAsync.GetPagedEateriesReponseAsync(validFilter);
            var data = entityEateries.data;
            RecordsCount recordCount = entityEateries.recordsCount;
            
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}
