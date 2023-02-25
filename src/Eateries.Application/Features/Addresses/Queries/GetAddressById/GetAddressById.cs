using AutoMapper;
using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Addresses.Queries.GetAddressById;

public class GetAddressById : IRequest<Response<Address>>
{
    public Guid Id { get; set; }
    
    public class GetAddressByIdHandler : IRequestHandler<GetAddressById, Response<Address>>
    {
        private readonly IAddressRepositoryAsync _addressRepositoryAsync;

        public GetAddressByIdHandler(IAddressRepositoryAsync addressRepositoryAsync)
        {
            _addressRepositoryAsync = addressRepositoryAsync;
        }
        public async Task<Response<Address>> Handle(GetAddressById request, CancellationToken cancellationToken)
        {
            var address = await _addressRepositoryAsync.GetByIdAsync(request.Id);
            if (address == null)
                throw new ApiException($"Address with {request.Id} not found");
            return new Response<Address>(address);
        }
    }
}