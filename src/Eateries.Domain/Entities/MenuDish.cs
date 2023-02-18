using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eateries.Domain.Common;

namespace Eateries.Domain.Entities;

public class MenuDish
{
    public Guid MenuId { get; set; }
    public Menu Menu { get; set; }

    public Guid DishId { get; set; }
    public Dish Dish { get; set; }
}