using Eateries.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Application.Features.Addresses.Commands
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
