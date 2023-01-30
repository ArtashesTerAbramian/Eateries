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
    internal class CreateMenuCommandValidator : AbstractValidator<Menu>
    {
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;

        public CreateMenuCommandValidator(IMenuRepositoryAsync menuRepositoryAsync)
        {
            this._menuRepositoryAsync = menuRepositoryAsync;

            RuleFor(r => r.Price)
                .NotEmpty().WithMessage("{PropertyName} cant be empty");
        }
    }
}
