using AutoMapper;
using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Parameters;
using Eateries.Application.Wrappers;
using Eateries.Domain.Entities;
using MediatR;

namespace Eateries.Application.Features.User.Queries.GetUsers;

public class GetUsersQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedResponse<IEnumerable<Entity>>>
{
    private readonly IUserRepositoryAsync _userRepositoryAsync;
    private readonly IModelHelper _modelHelper;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(
        IUserRepositoryAsync userRepositoryAsync,
        IModelHelper modelHelper,
        IMapper mapper)
    {
        _userRepositoryAsync = userRepositoryAsync;
        _modelHelper = modelHelper;
        _mapper = mapper;
    }
    public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var validFilter = request;
        //filtered fields security
        if (!string.IsNullOrEmpty(validFilter.Fields))
        {
            //limit to fields in view model
            validFilter.Fields = _modelHelper.ValidateModelFields<GetUserViewModel>(validFilter.Fields);
        }
        if (string.IsNullOrEmpty(validFilter.Fields))
        {
            //default fields from view model
            validFilter.Fields = _modelHelper.GetModelFields<GetUserViewModel>();
        }
        // query based on filter
        var userEntity = await _userRepositoryAsync.GetPagedUsersReponseAsync(validFilter);
        var data = userEntity.data;
        RecordsCount recordCount = userEntity.recordsCount;
        // response wrapper
        return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
    }
}