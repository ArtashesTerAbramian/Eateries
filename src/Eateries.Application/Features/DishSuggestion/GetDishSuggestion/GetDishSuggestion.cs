using Eateries.Application.Interfaces;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.DishSuggestion.GetDishSuggestion;

public class GetDishSuggestion : IRequest<Response<List<Dish>>>
{
    public Guid UserId { get; set; }
    public int numOfSuggestions { get; set; }

    public class GetDishSuggestionHandler : IRequestHandler<GetDishSuggestion, Response<List<Dish>>>
    {
        private readonly IMarkovChainFoodSuggestorService _dishSuggestionAsync;

        public GetDishSuggestionHandler(IMarkovChainFoodSuggestorService dishSuggestionAsync)
        {
            _dishSuggestionAsync = dishSuggestionAsync;
        }

        public async Task<Response<List<Dish>>> Handle(GetDishSuggestion request, CancellationToken cancellationToken)
        {
            var suggestion =
                await _dishSuggestionAsync.SuggestFood(request.UserId, request.numOfSuggestions);
            return new Response<List<Dish>>(suggestion);
        }
    }
}