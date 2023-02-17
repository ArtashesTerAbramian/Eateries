﻿using AutoMapper;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Application.Features.Menues.Commands
{
    public class CreateDishCommand : IRequest<Response<Guid>>
    {
        public string Name { get; set; }
    
        public int TimeMins { get; set; }
    
        public Guid CuisineId { get; set; }

        public string Instructions { get; set; }
    
        public string Description { get; set; }

        public string Note { get; set; }

        public decimal Price { get; set; }
    
        public string Image { get; set; }
    }

    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, Response<Guid>>
    {
        private readonly IDishRepositoryAsync _dishRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateDishCommandHandler(
            IDishRepositoryAsync dishRepositoryAsync,
            IMapper mapper)
        {
            this._dishRepositoryAsync = dishRepositoryAsync;
            this._mapper = mapper;
        }
        public async Task<Response<Guid>> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            var dish = _mapper.Map<Dish>(request);
            await _dishRepositoryAsync.AddAsync(dish);
            return new Response<Guid>(dish.Id);
        }
    }
}