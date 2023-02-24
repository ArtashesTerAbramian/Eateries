namespace Eateries.Application.Features.Menues.Queries.GetMenus
{
    public class GetMenuViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid EateryId { get; set; }
    }
}
