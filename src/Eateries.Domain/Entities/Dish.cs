using System.ComponentModel.DataAnnotations.Schema;
using Eateries.Domain.Common;

namespace Eateries.Domain.Entities;

public class Dish : AuditableBaseEntity
{
    public string Name { get; set; }
    
    public int TimeMins { get; set; }
    
    [ForeignKey("Coisine")]
    public Guid CoisineId { get; set; }
    public Cuisine Cuisine { get; set; }

    public string Instructions { get; set; }
    
    public string Description { get; set; }

    public string Note { get; set; }

    public decimal Price { get; set; }
    
    public byte Image { get; set; }
    
    
    
}