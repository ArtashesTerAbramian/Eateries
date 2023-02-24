using Eateries.Application.Interfaces.Repositories;
using FluentValidation;

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
