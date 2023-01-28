using AutoMapper;
using Eateries.Application.Features.Addresses.Commands;
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

namespace Eateries.Application.Features.Eateryes.Commands
{
    public class CreateEateryCommand : IRequest<Response<Guid>>
    {
        public string Name { get; set; }
        public EateryType? EateryType { get; set; }
        public Guid? Menu { get; set; }
        public Guid? AddressId { get; set; }
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

        async Task<Response<Guid>> IRequestHandler<CreateEateryCommand, Response<Guid>>.Handle(CreateEateryCommand request, CancellationToken cancellationToken)
        {
            var eatery = _mapper.Map<Eatery>(request);
            await _eateryRepositoryAsync.AddAsync(eatery);
            return new Response<Guid>(eatery.Id);
        }
    }
}
