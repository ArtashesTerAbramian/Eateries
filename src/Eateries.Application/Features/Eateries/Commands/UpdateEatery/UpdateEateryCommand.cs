using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using Eateries.Domain.Enums;
using MediatR;

namespace Eateries.Application.Features.Eateries.Commands.UpdateEatery;

public class UpdateEateryCommand : IRequest<Response<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid DishId { get; set; }
    public EateryType EateryType { get; set; }
    public int PlaceCount { get; set; }
    public int ChairPrice { get; set; }
    
    public class UpdateEateryCommandHandler : IRequestHandler<UpdateEateryCommand, Response<Guid>>
    {
        private readonly IEateryRepositoryAsync _eateryRepositoryAsync;

        public UpdateEateryCommandHandler(IEateryRepositoryAsync eateryRepositoryAsync)
        {
            _eateryRepositoryAsync = eateryRepositoryAsync;
        }
        public async Task<Response<Guid>> Handle(UpdateEateryCommand request, CancellationToken cancellationToken)
        {
            var eatery = await _eateryRepositoryAsync.GetByIdAsync(request.Id);

            if (eatery == null)
            {
                throw new ApiException("Eatery not found");
            }
            else
            {
                eatery.Name = request.Name;
                eatery.Description = request.Description;
                eatery.DishId = request.DishId;
                eatery.EateryType = request.EateryType;
                eatery.PlaceCount = request.PlaceCount;
                eatery.ChairPrice = request.ChairPrice;
            }

            await _eateryRepositoryAsync.UpdateAsync(eatery);
            return new Response<Guid>(eatery.Id);
        }
    }
}