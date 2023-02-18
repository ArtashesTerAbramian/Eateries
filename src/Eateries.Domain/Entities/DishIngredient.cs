using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eateries.Domain.Common;

namespace Eateries.Domain.Entities;

public class DishIngredient
{
    public Guid DishId { get; set; }
    public Dish Dish { get; set; }

    public Guid IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }
}