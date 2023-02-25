using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using MediatR;

namespace Eateries.Application.Features.User.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<Response<Guid>>
{
    public Guid Id { get; set; }
    
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<Guid>>
    {
        private readonly IUserRepositoryAsync _userRepositoryAsync;

        public DeleteUserCommandHandler(IUserRepositoryAsync userRepositoryAsync)
        {
            _userRepositoryAsync = userRepositoryAsync;
        }
        public async Task<Response<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepositoryAsync.GetByIdAsync(request.Id);
            if (user == null)
                throw new ApiException("User not found");
            await _userRepositoryAsync.DeleteAsync(user);
            return new Response<Guid>(request.Id);
        }
    }
}