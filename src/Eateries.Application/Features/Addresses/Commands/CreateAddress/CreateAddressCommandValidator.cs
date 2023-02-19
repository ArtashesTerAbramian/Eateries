using Eateries.Application.Interfaces.Repositories;
using FluentValidation;

namespace Eateries.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        private readonly IAddressRepositoryAsync addressRepositoryAsync;

        public CreateAddressCommandValidator(IAddressRepositoryAsync addressRepositoryAsync)
        {
            this.addressRepositoryAsync = addressRepositoryAsync;

            RuleFor(p => p.Street)
                .NotEmpty().WithMessage("{PropertyName} is requires")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }

    }
}
