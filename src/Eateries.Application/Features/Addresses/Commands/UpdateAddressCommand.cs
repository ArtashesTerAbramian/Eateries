using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Addresses.Commands;

public class UpdateAddressCommand : IRequest<Response<Guid>>
{
    public Guid Id { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }

    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, Response<Guid>>
    {
        private readonly IAddressRepositoryAsync _addressRepositoryAsync;

        public UpdateAddressCommandHandler(IAddressRepositoryAsync addressRepositoryAsync)
        {
            this._addressRepositoryAsync = addressRepositoryAsync;
        }
        public async Task<Response<Guid>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _addressRepositoryAsync.GetByIdAsync(request.Id);

            if (address == null)
            {
                throw new ApiException("Address not found");
            }
            else
            {
                address.City = request.City ?? address.City;
                address.Country = request.Country ?? address.Country;
                address.Street = request.Street ?? address.Street;
                await _addressRepositoryAsync.UpdateAsync(address);
                return new Response<Guid>(address.Id);
            }
        }
    } 
}