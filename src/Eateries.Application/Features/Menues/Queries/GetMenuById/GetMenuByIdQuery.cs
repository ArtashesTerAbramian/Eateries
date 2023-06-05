using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Menues.Queries.GetMenuById;

public class GetMenuByIdQuery : IRequest<Response<Menu>>
{
    public Guid Id { get; set; }

    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, Response<Menu>>
    {
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;

        public GetMenuByIdQueryHandler(IMenuRepositoryAsync menuRepositoryAsync)
        {
            _menuRepositoryAsync = menuRepositoryAsync;
        }
        public async Task<Response<Menu>> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var menu = await _menuRepositoryAsync.GetMenuById(request.Id);
            if (menu == null)
            {
                throw new ApiException("Entity does not found");
            }

            return new Response<Menu>(menu);
        }
    }

}