﻿using AutoMapper;
using Eateries.Application.Features.Positions.Queries.GetPositions;
using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Parameters;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Eateries.Application.Features.Addresses.Queries.GetAddresses
{
    public class GetAddressQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public string? street { get; set; }
        public string? country { get; set; }
        public string? city{ get; set; }
    }

    public class GetAllAddressQueryHandler : IRequestHandler<GetAddressQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IAddressRepositoryAsync addressRepository;
        private readonly IMapper mapper;
        private readonly IModelHelper modelHelper;

        public GetAllAddressQueryHandler(IAddressRepositoryAsync addressRepository, IMapper mapper, IModelHelper modelHelper)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
            this.modelHelper = modelHelper;
        }


        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetAddressQuery request,
                                                               CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = modelHelper.ValidateModelFields<GetAddressViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = modelHelper.GetModelFields<GetAddressViewModel>();
            }
            // query based on filter
            var entityPositions = await addressRepository.GetPagedAddressReponseAsync(validFilter);
            var data = entityPositions.data;
            RecordsCount recordCount = entityPositions.recordsCount;
            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}