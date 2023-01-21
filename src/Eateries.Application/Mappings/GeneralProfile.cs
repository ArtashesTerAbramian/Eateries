using AutoMapper;
using Eateries.Application.Features.Addresses.Commands;
using Eateries.Application.Features.Employees.Queries.GetEmployees;
//using Eateries.Application.Features.Positions.Commands.CreatePosition;
using Eateries.Application.Features.Positions.Queries.GetPositions;
using Eateries.Domain.Entities;

namespace Eateries.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Position, GetPositionsViewModel>().ReverseMap();
            CreateMap<Employee, GetEmployeesViewModel>().ReverseMap();
            //CreateMap<CreatePositionCommand, Address>();
            CreateMap<CreateAddressCommand, Address>();
        }
    }
}