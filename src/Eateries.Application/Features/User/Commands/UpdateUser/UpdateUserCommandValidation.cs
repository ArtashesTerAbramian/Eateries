using Eateries.Application.Features.User.Commands.CreateUser;
using Eateries.Application.Interfaces.Repositories;
using FluentValidation;

namespace Eateries.Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
{
    private readonly IUserRepositoryAsync _userRepositoryAsync;

    public UpdateUserCommandValidation(IUserRepositoryAsync userRepositoryAsync)
    {
        _userRepositoryAsync = userRepositoryAsync;


        RuleFor(x => x.FirstName)
            .MaximumLength(50);
        RuleFor(x => x.LastName)
            .MaximumLength(50);
        RuleFor(x => x.Email)
            .EmailAddress();
        RuleFor(x => x.Password)
            .MinimumLength(8);

    }
}