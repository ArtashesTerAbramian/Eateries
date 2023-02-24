using System.ComponentModel.DataAnnotations;

namespace Eateries.Domain.Entities
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public Guid EateryId { get; set; }
        public Eatery Eatery { get; set; }
    }
}