using FluentValidation;

namespace Eateries.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidation()
    {
        RuleFor(r => r.EateryId)
            .NotEmpty().WithMessage("{PropertyName}cant be empty");
        RuleFor(r => r.UserId)  
            .NotEmpty().WithMessage("{PropertyName}cant be empty");
    }
}