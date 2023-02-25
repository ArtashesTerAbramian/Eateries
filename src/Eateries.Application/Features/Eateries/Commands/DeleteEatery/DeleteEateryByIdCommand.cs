using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using MediatR;

namespace Eateries.Application.Features.Eateries.Commands.DeleteEatery;

public class DeleteEateryByIdCommand : IRequest<Response<Guid>>
{
    public Guid Id { get; set; }

    public class DeleteEateryByIdCommandHandler : IRequestHandler<DeleteEateryByIdCommand, Response<Guid>>
    {
        private readonly IEateryRepositoryAsync _eateryRepositoryAsync;

        public DeleteEateryByIdCommandHandler(IEateryRepositoryAsync eateryRepositoryAsync)
        {
            _eateryRepositoryAsync = eateryRepositoryAsync;
        }

        public async Task<Response<Guid>> Handle(DeleteEateryByIdCommand request, CancellationToken cancellationToken)
        {
            var eatery = await _eateryRepositoryAsync.GetByIdAsync(request.Id);
            if (eatery == null)
                throw new ApiException("Eatery not found");
            await _eateryRepositoryAsync.DeleteAsync(eatery);
            return new Response<Guid>(eatery.Id);
        }
    }
}