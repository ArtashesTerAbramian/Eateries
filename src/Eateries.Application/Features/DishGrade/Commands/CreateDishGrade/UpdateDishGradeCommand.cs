using AutoMapper;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Application.Wrappers;
using MediatR;

namespace Eateries.Application.Features.DishGrade.Commands.CreateDishGrade;

public class UpdateDishGradeCommand : IRequest<Response<Domain.Entities.DishGrade>>
{
    public Guid DishId { get; set; }
    public Guid MenuId { get; set; }
    public int Grade { get; set; }
    public string? Comment { get; set; }
}

public class
    UpdateDishGradeCommandHandler : IRequestHandler<UpdateDishGradeCommand, Response<Domain.Entities.DishGrade>>
{
    private readonly IDishGradeRepositoryAsync _dishGradeRepositoryAsync;

    public UpdateDishGradeCommandHandler(IDishGradeRepositoryAsync dishGradeRepositoryAsync)
    {
        _dishGradeRepositoryAsync = dishGradeRepositoryAsync;
    }

    public async Task<Response<Domain.Entities.DishGrade>> Handle(UpdateDishGradeCommand request,
        CancellationToken cancellationToken)
    {
        var dishGrade = await _dishGradeRepositoryAsync.GetGradeByIds(request.MenuId, request.DishId);
        if (dishGrade == null)
        {
            var newDishGrade = new Domain.Entities.DishGrade
            {
                DishId = request.DishId,
                MenuId = request.MenuId,
                Grade = request.Grade,
                Comment = request.Comment
            };
            await _dishGradeRepositoryAsync.AddAsync(newDishGrade);
            return new Response<Domain.Entities.DishGrade>(newDishGrade);
        }

        dishGrade.Grade = request.Grade;
        await _dishGradeRepositoryAsync.UpdateAsync(dishGrade);
        return new Response<Domain.Entities.DishGrade>(dishGrade);
    }
}