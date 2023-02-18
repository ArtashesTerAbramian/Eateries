using Eateries.Domain.Common;
using Eateries.Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Eateries.Domain.Entities
{
    public class Eatery : AuditableBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }
        public Menu? Menu { get; set; }
        public Address? Address { get; set; }

        public ICollection<Menu> Menus { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public EateryType? EateryType { get; set; }

        public int? PlaceCount { get; set; }

        public int? ChairPrice { get; set; }
    }
}