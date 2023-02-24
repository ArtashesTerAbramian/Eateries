using Eateries.Domain.Common;
using Eateries.Domain.Enums;

namespace Eateries.Domain.Entities;

public class OrderHistory : AuditableBaseEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid EateryId { get; set; }
    public Eatery Eatery { get; set; }
    
    public DateTime OrderDate { get; set; }
    public decimal TotalCost { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CompletedDate { get; set; }
}