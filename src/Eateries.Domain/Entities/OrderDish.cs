namespace Eateries.Domain.Entities;

public class OrderDish
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    public Guid DishId { get; set; }
    public Dish Dish { get; set; }

    public int Quantity { get; set; }
}