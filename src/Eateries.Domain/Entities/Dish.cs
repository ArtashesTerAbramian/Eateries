using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eateries.Domain.Common;

namespace Eateries.Domain.Entities;

public class Dish : AuditableBaseEntity
{
    [Key]
    public override Guid Id { get; set; }
    public string Name { get; set; }

    public int TimeMins { get; set; }

    [ForeignKey("Cuisine")]
    public Guid CuisineId { get; set; }
    public Cuisine Cuisine { get; set; }

    public string Instructions { get; set; }

    public string Description { get; set; }

    public string Note { get; set; }

    public decimal Price { get; set; }

    public string ImageUrl { get; set; }

    public ICollection<MenuDish> MenuDishes { get; set; }
    public ICollection<DishIngredient> DishIngredients { get; set; }
}