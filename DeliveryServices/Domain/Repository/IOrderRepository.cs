using DeliveryServices.Services.Dto;

namespace DeliveryServices.Domain.Repository
{
    internal interface IOrderRepository
    {
        public List<OrderDto> FilterOrdersByDistrictAndTime(string district, DateTime begin, DateTime end);
    }
}
