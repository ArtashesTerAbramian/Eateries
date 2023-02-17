using AutoMapper;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using Eateries.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eateries.Application.Exceptions;

namespace Eateries.Application.Features.Eateries.Commands
{
    public class CreateEateryCommand : IRequest<Response<Guid>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid DishId { get; set; }
        public EateryType? EateryType { get; set; }
        public int? PlaceCount { get; set; }
        public int? ChairPrice { get; set; }
    }

    public class CreateEateryCommandHandler : IRequestHandler<CreateEateryCommand, Response<Guid>>
    {
        private readonly IEateryRepositoryAsync _eateryRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly IDishRepositoryAsync _dishRepositoryAsync;

        public CreateEateryCommandHandler(
            IEateryRepositoryAsync eateryRepositoryAsync,
            IMapper mapper,
            IDishRepositoryAsync dishRepositoryAsync)
        {
            this._eateryRepositoryAsync = eateryRepositoryAsync;
            this._mapper = mapper;
            this._dishRepositoryAsync = dishRepositoryAsync;
        }
        public async Task<Response<Guid>> Handle(CreateEateryCommand request, CancellationToken cancellationToken)
        {
            var dish = await _dishRepositoryAsync.GetByIdAsync(request.DishId);
            if (dish == null)
                throw new ApiException("DishId is not vlaid");
            
            var eatery = _mapper.Map<Eatery>(request);
            await _eateryRepositoryAsync.AddAsync(eatery);
            return new Response<Guid>(eatery.Id);
        }
    }
}
