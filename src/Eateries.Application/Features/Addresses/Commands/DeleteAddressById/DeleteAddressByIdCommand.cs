using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using MediatR;

namespace Eateries.Application.Features.Addresses.Commands.DeleteAddressById;

public class DeleteAddressByIdCommand : IRequest<Response<Guid>>
{
    public Guid Id { get; set; }
    
    public class DeleteAddressByIdCommandHandler : IRequestHandler<DeleteAddressByIdCommand, Response<Guid>>
    {
        private readonly IAddressRepositoryAsync _addressRepositoryAsync;

        public DeleteAddressByIdCommandHandler(IAddressRepositoryAsync addressRepositoryAsync)
        {
            _addressRepositoryAsync = addressRepositoryAsync;
        }
        public async Task<Response<Guid>> Handle(DeleteAddressByIdCommand request, CancellationToken cancellationToken)
        {
            var address = await _addressRepositoryAsync.GetByIdAsync(request.Id);
            if (address == null)
                throw new ApiException($"Address with ID: {request.Id} not found");
            await _addressRepositoryAsync.DeleteAsync(address);
            return new Response<Guid>(request.Id);
        }
    }
}