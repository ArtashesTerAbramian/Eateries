using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eateries.Domain.Common;

namespace Eateries.Domain.Entities;

public class Ingredient : AuditableBaseEntity
{
    [Key]
    public override Guid Id { get; set; }

    public string IngredientName { get; set; }
    public ICollection<DishIngredient> DishIngredients { get; set; }
}