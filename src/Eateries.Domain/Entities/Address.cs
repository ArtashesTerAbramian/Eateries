using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Eateries.Domain.Entities
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public Guid EateryId { get; set; }
        public Eatery Eatery { get; set; }
    }
}
