using AutoMapper;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Menues.Commands.CreateMenu
{
    public class CreateMenuCommand : IRequest<Response<Guid>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid EateryId { get; set; }
    }

    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, Response<Guid>>
    {
        private readonly IMenuRepositoryAsync _menuRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateMenuCommandHandler(IMenuRepositoryAsync menuRepositoryAsync, IMapper mapper)
        {
            this._menuRepositoryAsync = menuRepositoryAsync;
            this._mapper = mapper;
        }
        public async Task<Response<Guid>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = _mapper.Map<Menu>(request);
            await _menuRepositoryAsync.AddAsync(menu);
            return new Response<Guid>(menu.Id);
        }
    }
}
