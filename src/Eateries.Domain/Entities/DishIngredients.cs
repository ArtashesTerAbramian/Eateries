using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eateries.Domain.Common;

namespace Eateries.Domain.Entities;

public class DishIngredients : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid Id { get; set; }
    
    [ForeignKey("Dish")]
    public Guid DishId { get; set; }
    public Dish Dish { get; set; }
    
    [ForeignKey("Ingredient")] 
    public Guid IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }
}