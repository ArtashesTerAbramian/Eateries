using Eateries.Domain.Common;

namespace Eateries.Domain.Entities;

public class Cuisine : AuditableBaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Dish> Dishes { get; set; }
}