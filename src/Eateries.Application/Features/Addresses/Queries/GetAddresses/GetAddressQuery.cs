﻿using AutoMapper;
using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Parameters;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Addresses.Queries.GetAddresses
{
    public class GetAddressQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public string? Street { get; set; }
        public string? Country { get; set; }
        public string? City{ get; set; }
        public Guid EateryId { get; set; }
    }

    public class GetAllAddressQueryHandler : IRequestHandler<GetAddressQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IAddressRepositoryAsync _addressRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetAllAddressQueryHandler(
            IAddressRepositoryAsync addressRepository, 
            IMapper mapper,
            IModelHelper modelHelper)
        {
            this._addressRepositoryAsync = addressRepository;
            this._mapper = mapper;
            this._modelHelper = modelHelper;
        }


        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetAddressQuery request,
                                                               CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetAddressViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetAddressViewModel>();
            }
            // query based on filter
            var entityAddress = await _addressRepositoryAsync.GetPagedAddressReponseAsync(validFilter);
            var data = entityAddress.data;
            RecordsCount recordCount = entityAddress.recordsCount;
            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}
