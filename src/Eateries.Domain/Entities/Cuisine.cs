using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eateries.Domain.Common;

namespace Eateries.Domain.Entities;

public class Cuisine : AuditableBaseEntity
{
    public string CuisineName { get; set; }
    public List<Dish> Dishes { get; set; }
}