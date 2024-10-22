using DeliveryServices.Services.Dto;

namespace DeliveryServices.Service
{
    internal interface IOrderService
    {
        public List<OrderDto> FilterOrdersByDistrictAndTime(string district, string format, string begin, string end);
    }
}
