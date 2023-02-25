using AutoMapper;
using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Services;
using Eateries.Application.Wrappers;
using MediatR;

namespace Eateries.Application.Features.User.Commands.CreateUser;

public class CreateUserCommand : IRequest<Response<Guid>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<Guid>>
{
    private readonly IUserRepositoryAsync _userRepositoryAsync;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepositoryAsync userRepositoryAsync, IMapper mapper)
    {
        _userRepositoryAsync = userRepositoryAsync;
        _mapper = mapper;
    }
    public async Task<Response<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepositoryAsync.GetUserByEmailAsync(request.Email);
        if (user != null)
        {
            throw new ApiException("The user with this email already exists");
        }
        var hasher = new PasswordHashingService();
        
        user = _mapper.Map<Domain.Entities.User>(request);
        user.PasswordHash = hasher.HashPassword(request.Password);
        user.IsActive = true;
        
        await _userRepositoryAsync.AddAsync(user);

        
        return new Response<Guid>(user.Id);
    }
}