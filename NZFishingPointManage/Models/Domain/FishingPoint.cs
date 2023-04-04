namespace NZFishingPointManage.Models.Domain
{
    public class FishingPoint
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        
        public string? AddressPicture { get; set; }
    }
}
