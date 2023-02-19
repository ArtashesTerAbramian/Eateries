using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eateries.Domain.Common;

namespace Eateries.Domain.Entities;

public class Cuisine : AuditableBaseEntity
{
    [Key]
    public override Guid Id { get; set; }
    public string CuisineName { get; set; }
    public ICollection<Dish> Dishes { get; set; }
}