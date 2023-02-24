using System.ComponentModel.DataAnnotations;
using Eateries.Domain.Common;
using Eateries.Domain.Enums;

namespace Eateries.Domain.Entities;

public class OrderHistory : AuditableBaseEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    
    public Guid EateryId { get; set; }
    public Eatery Eatery { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    [EnumDataType(typeof(OrderStatus))]
    public OrderStatus Status { get; set; }
    public DateTime CompletedDate { get; set; }
}