﻿using AutoMapper;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using Eateries.Domain.Enums;
using MediatR;

namespace Eateries.Application.Features.Eateries.Commands.CreateEatery
{
    public class CreateEateryCommand : IRequest<Response<Guid>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public EateryType? EateryType { get; set; }
        public int? PlaceCount { get; set; }
        public int? ChairPrice { get; set; }
    }

    public class CreateEateryCommandHandler : IRequestHandler<CreateEateryCommand, Response<Guid>>
    {
        private readonly IEateryRepositoryAsync _eateryRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateEateryCommandHandler(IEateryRepositoryAsync eateryRepositoryAsync, IMapper mapper)
        {
            this._eateryRepositoryAsync = eateryRepositoryAsync;
            this._mapper = mapper;
        }
        public async Task<Response<Guid>> Handle(CreateEateryCommand request, CancellationToken cancellationToken)
        {
            var eatery = _mapper.Map<Eatery>(request);
            await _eateryRepositoryAsync.AddAsync(eatery);
            return new Response<Guid>(eatery.Id);
        }
    }
}
