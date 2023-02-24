using Eateries.Domain.Common;
using Eateries.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Eateries.Domain.Entities
{
    public class Eatery : AuditableBaseEntity
    {
        public string Name { get; set; }

        public string? Description { get; set; }
        public List<Menu> Menus { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderHistory> OrderHistories { get; set; }
        
        [EnumDataType(typeof(EateryType))]
        public EateryType? EateryType { get; set; }

        public int? PlaceCount { get; set; }

        public int? ChairPrice { get; set; }
    }
}