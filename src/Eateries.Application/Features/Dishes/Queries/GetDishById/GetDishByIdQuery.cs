using AutoMapper;
using Eateries.Application.Exceptions;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.Dishes.Queries.GetDishById;

public class GetDishByIdQuery : IRequest<Response<GetDishByIdViewModel>> 
{
    public Guid Id { get; set; }
    
    public class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, Response<GetDishByIdViewModel>>
    {
        private readonly IDishRepositoryAsync _dishRepositoryAsync;
        private readonly IMapper _mapper;

        public GetDishByIdQueryHandler(IDishRepositoryAsync dishRepositoryAsync, IMapper mapper)
        {
            _dishRepositoryAsync = dishRepositoryAsync;
            _mapper = mapper;
        }
        
        public async Task<Response<GetDishByIdViewModel>> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            var dish = await _dishRepositoryAsync.GetByIdAsync(request.Id);
            if (dish == null)
                throw new ApiException($"Dish with {request.Id} ID not found");
            var dishView = _mapper.Map<GetDishByIdViewModel>(dish);
            return new Response<GetDishByIdViewModel>(dishView);
        }
    }
}