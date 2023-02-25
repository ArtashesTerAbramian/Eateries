using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Eateries.Queries.GetEateryById;

public class GetEateryByQueryId : IRequest<Response<Eatery>>
{
    public Guid Id { get; set; }

    public class GetEateryByIdQueryHandler : IRequestHandler<GetEateryByQueryId, Response<Eatery>>
    {
        private readonly IEateryRepositoryAsync _eateryRepositoryAsync;

        public GetEateryByIdQueryHandler(IEateryRepositoryAsync eateryRepositoryAsync)
        {
            _eateryRepositoryAsync = eateryRepositoryAsync;
        }
        public async Task<Response<Eatery>> Handle(GetEateryByQueryId request, CancellationToken cancellationToken)
        {
            var eatery = await _eateryRepositoryAsync.GetByIdAsync(request.Id);
            if (eatery == null)
                throw new ApiException("Eatery not found");

            return new Response<Eatery>(eatery);
        }
    }
}