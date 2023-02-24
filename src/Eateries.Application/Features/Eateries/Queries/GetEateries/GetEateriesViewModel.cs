using Eateries.Domain.Enums;

namespace Eateries.Application.Features.Eateries.Queries.GetEateries
{
    internal class GetEateriesViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public EateryType? EateryType { get; set; }
        public int? PlaceCount { get; set; }
        public int? ChairPrice { get; set; }
    }
}
