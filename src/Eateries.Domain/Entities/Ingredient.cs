using Eateries.Domain.Common;

namespace Eateries.Domain.Entities;

public class Ingredient : AuditableBaseEntity
{
    public string IngredientName { get; set; }
    public List<DishIngredient> DishIngredients { get; set; }
}