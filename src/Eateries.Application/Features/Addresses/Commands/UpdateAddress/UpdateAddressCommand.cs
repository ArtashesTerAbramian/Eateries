using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using MediatR;

namespace Eateries.Application.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommand : IRequest<Response<Guid>>
{
    public Guid Id { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public Guid? EateryId { get; set; }
    
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, Response<Guid>>
    {
        private readonly IAddressRepositoryAsync _addressRepositoryAsync;

        public UpdateAddressCommandHandler(IAddressRepositoryAsync addressRepositoryAsync)
        {
            _addressRepositoryAsync = addressRepositoryAsync;
        }
        public async Task<Response<Guid>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _addressRepositoryAsync.GetByIdAsync(request.Id);
            if (address == null)
                throw new ApiException("Address not found");
            else
            {
                if (request.Country != null)
                    address.Country = request.Country;
                if(request.City != null)
                    address.City = request.City;
                if(request.Street != null)
                    address.Street = request.Street;
                if (request.EateryId != null)
                {
                    string eateryId = request.EateryId.HasValue ? request.EateryId.Value.ToString() : null;
                    address.EateryId = new Guid(eateryId);
                }

                await _addressRepositoryAsync.UpdateAsync(address);

                return new Response<Guid>(address.Id);
            }
        }
    }
}