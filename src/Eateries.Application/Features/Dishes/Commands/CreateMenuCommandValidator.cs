using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Application.Features.Menues.Commands
{
    internal class CreateMenuCommandValidator : AbstractValidator<Dish>
    {
        private readonly IDishRepositoryAsync _dishRepositoryAsync;

        public CreateMenuCommandValidator(IDishRepositoryAsync dishRepositoryAsync)
        {
            this._dishRepositoryAsync = dishRepositoryAsync;

            RuleFor(r => r.Price)
                .NotEmpty().WithMessage("{PropertyName} cant be empty");
        }
    }
}
