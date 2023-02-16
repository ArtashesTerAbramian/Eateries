using Eateries.Domain.Common;
using Eateries.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Domain.Entities
{
    public class Eatery : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey("Dish")]
        public Guid DishId { get; set; }
        public Dish? Dish { get; set; }

        [ForeignKey("Address")]
        public Guid AddressId { get; set; }
        public Address? Address { get; set; }
        public EateryType? EateryType { get; set; }
        public int? PlaceCount { get; set; }
        public int? ChairPrice { get; set; }
    }
}
