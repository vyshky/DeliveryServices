namespace DeliveryServices.Services.Dto
{
    internal class OrderDto
    {
        public Guid Id { get; set; }
        public float Weight { get; set; }
        public string District { get; set; }
        public DateTime DeliveryTime { get; set; }
    }
}
