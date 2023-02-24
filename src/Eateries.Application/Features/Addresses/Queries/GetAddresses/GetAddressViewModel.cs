namespace Eateries.Application.Features.Addresses.Queries.GetAddresses
{
    internal class GetAddressViewModel
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public Guid EateryId { get; set; }
    }
}
