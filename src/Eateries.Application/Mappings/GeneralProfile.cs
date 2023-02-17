﻿using AutoMapper;
using Eateries.Application.Features.Addresses.Commands;
using Eateries.Application.Features.Addresses.Queries.GetAddresses;
using Eateries.Application.Features.Cuisine.Command.CreateCuisine;
using Eateries.Application.Features.Eateries.Commands;
using Eateries.Application.Features.Eateries.Queries.GetEateries;
using Eateries.Application.Features.Menues.Commands;
using Eateries.Application.Features.Menues.Queries;
using Eateries.Domain.Entities;

namespace Eateries.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateAddressCommand, Address>();
            CreateMap<Address, GetAddressViewModel>().ReverseMap();
            CreateMap<CreateDishCommand, Dish>().ReverseMap();
            CreateMap<Dish, GetDishViewModel>().ReverseMap();
            CreateMap<CreateEateryCommand, Eatery>();
            CreateMap<Eatery, GetEateriesViewModel>().ReverseMap();
            CreateMap<Cuisine, CreateCuisineCommand>().ReverseMap();
            
        }
    }
}