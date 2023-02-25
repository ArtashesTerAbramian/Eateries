using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using MediatR;
using Eateries.Domain.Entities;

namespace Eateries.Application.Features.User.Queries.GetUserByIdQuery;

public class GetUserByIdQuery : IRequest<Response<Domain.Entities.User>>
{
    public Guid Id { get; set; }
    
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<Domain.Entities.User>>
    {
        private readonly IUserRepositoryAsync _userRepositoryAsync;

        public GetUserByIdQueryHandler(IUserRepositoryAsync userRepositoryAsync)
        {
            _userRepositoryAsync = userRepositoryAsync;
        }
        public async Task<Response<Domain.Entities.User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepositoryAsync.GetByIdAsync(request.Id);
            if (user == null)
                throw new ApiException("User not found");
            return new Response<Domain.Entities.User>(user);
        }
    }
}