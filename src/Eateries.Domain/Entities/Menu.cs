using Eateries.Domain.Common;

namespace Eateries.Domain.Entities
{
    public class Menu : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public Guid EateryId { get; set; }
        public Eatery Eatery { get; set; }

        public List<MenuDish> MenuDishes { get; set; }
        public List<DishGrade> DishGrades { get; set; }
    }
}