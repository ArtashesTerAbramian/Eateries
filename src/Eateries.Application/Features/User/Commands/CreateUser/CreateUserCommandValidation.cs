using Eateries.Application.Interfaces.Repositories;
using FluentValidation;

namespace Eateries.Application.Features.User.Commands.CreateUser;

public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepositoryAsync _userRepositoryAsync;

    public CreateUserCommandValidation(IUserRepositoryAsync userRepositoryAsync)
    {
        _userRepositoryAsync = userRepositoryAsync;

        RuleFor(r => r.FirstName)
            .NotEmpty().WithMessage("{PropertyName} can't be empty");
        RuleFor(r => r.LastName)
            .NotEmpty().WithMessage("{PropertyName} can't be empty");
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("{PropertyName} can't be empty");
        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("{PropertyName} can't be empty");
    }
}