using AutoMapper;
using Eateries.Application.Features.Addresses.Commands.CreateAddress;
using Eateries.Application.Features.Addresses.Queries.GetAddresses;
using Eateries.Application.Features.Cuisines.Queries.GetCuisines;
using Eateries.Application.Features.Dishes.Commands;
using Eateries.Application.Features.Eateries.Commands.CreateEatery;
using Eateries.Application.Features.Eateries.Queries.GetEateries;
using Eateries.Application.Features.Menues.Commands.CreateMenu;
using Eateries.Application.Features.Menues.Queries.GetMenus;
using Eateries.Application.Features.Orders.Commands.CreateOrder;
//using Eateries.Application.Features.Positions.Commands.CreatePosition;
using Eateries.Application.Features.User.Commands.CreateUser;
using Eateries.Domain.Entities;

namespace Eateries.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateAddressCommand, Address>();
            CreateMap<Address, GetAddressViewModel>().ReverseMap();
            CreateMap<CreateMenuCommand, Menu>().ReverseMap();
            CreateMap<Menu, GetMenuViewModel>().ReverseMap();
            CreateMap<CreateEateryCommand, Eatery>();
            CreateMap<Eatery, GetEateriesViewModel>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<Cuisine, GetCuisinesQuery>().ReverseMap();
            CreateMap<Order, CreateOrderCommand>().ReverseMap();
            CreateMap<Dish, CreateDishCommand>().ReverseMap();
        }
    }
}