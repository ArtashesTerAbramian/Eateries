using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Application.Features.Eateryes.Commands
{
    internal class CreateEateryCommandValidation : AbstractValidator<Eatery>
    {
        private readonly IEateryRepositoryAsync _eateryRepositoryAsync;

        public CreateEateryCommandValidation(IEateryRepositoryAsync eateryRepositoryAsync)
        {
            this._eateryRepositoryAsync = eateryRepositoryAsync;

            RuleFor(r => r.Address)
                .NotEmpty().WithMessage("{PropertyName} can not be empaty!")
                .NotNull();
        }

    }
}
