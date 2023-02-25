using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Services;
using Eateries.Application.Wrappers;
using MediatR;

namespace Eateries.Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<Response<Guid>>
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool? IsActive { get; set; }

    public class UpdateUserHandlerCommand : IRequestHandler<UpdateUserCommand, Response<Guid>>
    {
        private readonly IUserRepositoryAsync _userRepositoryAsync;

        public UpdateUserHandlerCommand(IUserRepositoryAsync userRepositoryAsync)
        {
            _userRepositoryAsync = userRepositoryAsync;
        }

        public async Task<Response<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepositoryAsync.GetByIdAsync(request.Id);
            if (user == null)
                throw new ApiException("User does not found");

            if (!String.IsNullOrEmpty(request.Email))
            {
                var userByEmail = await _userRepositoryAsync.GetUserByEmailAsync(request.Email);
                if (userByEmail != null)
                    throw new ApiException("User with this email alread exisit");
                user.Email = request.Email;
            }

            if (!String.IsNullOrEmpty(request.LastName))
                user.LastName = request.LastName;
            if (!String.IsNullOrEmpty(request.FirstName))
                user.FirstName = request.FirstName;
            if (!String.IsNullOrEmpty(request.Password))
            {
                var hasher = new PasswordHashingService();
                string hashPassword = hasher.HashPassword(request.Password);
                user.PasswordHash = hashPassword;
            }

            if (request.IsActive != null)
            {
                bool isActive = request.IsActive.Value;
                user.IsActive = isActive;
            }

            return new Response<Guid>(user.Id);
        }
    }
}