using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Addresses.Queries.GetAddressById;

public class GetAddressByIdQuery : IRequest<Response<Address>>
{
    public Guid Id { get; set; }

    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, Response<Address>>
    {
        public IAddressRepositoryAsync _addressRepositoryAsync { get; set; }

        public GetAddressByIdQueryHandler(IAddressRepositoryAsync addressRepositoryAsync)
        {
            _addressRepositoryAsync = addressRepositoryAsync;
        }
        
        public async Task<Response<Address>> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var address = await _addressRepositoryAsync.GetByIdAsync(request.Id);
            if (address == null) throw new ApiException($"Address Not Found.");
            return new Response<Address>(address);
        }
    }
}