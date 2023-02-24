using AutoMapper;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Addresses.Commands.CreateAddress
{
    public partial class CreateAddressCommand : IRequest<Response<Guid>>
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public Guid EateryId { get; set; }
    }

    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Response<Guid>>
    {
        private readonly IAddressRepositoryAsync repositoryAsync;
        private readonly IMapper mapper;

        public CreateAddressCommandHandler(IAddressRepositoryAsync repositoryAsync, IMapper mapper)
        {
            this.repositoryAsync = repositoryAsync;
            this.mapper = mapper;
        }
        public async Task<Response<Guid>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = mapper.Map<Address>(request);
            await repositoryAsync.AddAsync(address);
            return new Response<Guid>(address.Id);
        }
    }

}
