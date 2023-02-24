using System.ComponentModel.DataAnnotations;
using Eateries.Domain.Common;
using Eateries.Domain.Enums;

namespace Eateries.Domain.Entities;

public class Order : AuditableBaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid EateryId { get; set; }
    public Eatery Eatery { get; set; }
    
    public DateTime OrderDate { get; set; }
    public decimal TotalCost { get; set; }
    
    [EnumDataType(typeof(OrderStatus))]
    public OrderStatus Status { get; set; }
    public DateTime CompletedDate { get; set; }
    public List<Dish> Dishes { get; set; }
    public List<OrderHistory> OrderHistories { get; set; }
}