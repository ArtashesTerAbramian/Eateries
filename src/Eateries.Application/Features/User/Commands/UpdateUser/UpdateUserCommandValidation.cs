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