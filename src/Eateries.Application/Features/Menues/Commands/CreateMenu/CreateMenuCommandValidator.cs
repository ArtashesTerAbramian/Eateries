﻿using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using FluentValidation;

namespace Eateries.Application.Features.Menues.Commands.CreateMenu
{
    internal class CreateMenuCommandValidator : AbstractValidator<Menu>
    {
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;

        public CreateMenuCommandValidator(IMenuRepositoryAsync menuRepositoryAsync)
        {
            this._menuRepositoryAsync = menuRepositoryAsync;

            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("{PropertyName} cant be empty");
        }
    }
}
