using Eateries.Application.Interfaces.Repositories;
using FluentValidation;

namespace Eateries.Application.Features.User.Commands.CreateUser;

public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepositoryAsync _userRepositoryAsync;

    public CreateUserCommandValidation(IUserRepositoryAsync userRepositoryAsync)
    {
        _userRepositoryAsync = userRepositoryAsync;


        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);

    }
}