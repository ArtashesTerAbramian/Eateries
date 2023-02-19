using Eateries.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Application.Features.Eateries.Commands.CreateEatery
{
    internal class CreateEateryCommandValidation : AbstractValidator<CreateEateryCommand>
    {
        private readonly IEateryRepositoryAsync _eateryRepositoryAsync;
        public CreateEateryCommandValidation(IEateryRepositoryAsync eateryRepositoryAsync)
        {
            this._eateryRepositoryAsync = eateryRepositoryAsync;
        }
    }
}
