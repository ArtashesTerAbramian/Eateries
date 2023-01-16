using Eateries.Domain.Common;
using Eateries.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Domain.Entities
{
    public abstract class Eateries : AuditableBaseEntity
    {
        public string Name { get; set; }
        public EateryType? EateryType { get; set; }
        public Menu Menu { get; set; }
        public Address Address { get; set; }
    }
}
