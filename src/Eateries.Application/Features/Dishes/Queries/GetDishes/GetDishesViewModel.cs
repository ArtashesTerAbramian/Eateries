namespace Eateries.Application.Features.Dishes.Queries.GetDishes;

public class GetDishesViewModel
{
    public string Name { get; set; }

    public int TimeMins { get; set; }

    public Guid CuisineId { get; set; }

    public string Instructions { get; set; }

    public string Description { get; set; }

    public string Note { get; set; }

    public decimal Price { get; set; }

    public string ImageUrl { get; set; }
}