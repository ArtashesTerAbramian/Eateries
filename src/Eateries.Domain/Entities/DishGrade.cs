namespace Eateries.Domain.Entities;

public class DishGrade
{
    public Guid DishId { get; set; }
    public Dish Dish { get; set; }
    public Guid MenuId { get; set; }
    public Menu Menu { get; set; }
    public int Grade { get; set; }
    public string? Comment { get; set; }
}