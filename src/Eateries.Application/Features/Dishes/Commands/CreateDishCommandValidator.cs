using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Application.Features.Menues.Commands
{
    internal class CreateDishCommandValidator : AbstractValidator<Dish>
    {
        private readonly IDishRepositoryAsync _dishRepositoryAsync;

        public CreateDishCommandValidator(IDishRepositoryAsync dishRepositoryAsync)
        {
            this._dishRepositoryAsync = dishRepositoryAsync;

            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("{PropertyName} can't be empty");
                //.MustAsync(IsUniqueDishName).WithMessage("{PropertyName} already exists");
            
            RuleFor(r => r.Price)
                .NotEmpty().WithMessage("{PropertyName} cant be empty");
        }

        /*private async Task<bool> IsUniqueDishName(string name, CancellationToken cancellationToken)
        {
            return await _dishRepositoryAsync.IsUniqueDishNumberAsync(name);
        }*/
    }
}
