using Eateries.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Eateries.Domain.Entities
{
    public class Menu : AuditableBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public Guid EateryId { get; set; }
        public Eatery Eatery { get; set; }

        public ICollection<MenuDish> MenuDishes { get; set; }
    }
}