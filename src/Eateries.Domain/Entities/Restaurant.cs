using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Domain.Entities
{
    public class Restaurant : Eatery
    {
        public int? PlaceCount { get; set; }
        public int? ChairPrice { get; set; }
    }
}
