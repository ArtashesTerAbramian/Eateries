using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eateries.Domain.Common;

namespace Eateries.Domain.Entities;

public class MenuDish : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid Id { get; set; }
    public Guid MenuId { get; set; }
    public Menu Menu { get; set; }

    public Guid DishId { get; set; }
    public Dish Dish { get; set; }
}