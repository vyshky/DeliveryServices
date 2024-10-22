using DeliveryServices.Domain.Repository;
using DeliveryServices.Services.Dto;
using System.Data;
using System.Globalization;

namespace DeliveryServices.Service
{
    internal class OrderService : IOrderService
    {
        IOrderRepository orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public List<OrderDto> FilterOrdersByDistrictAndTime(string district, string format, string begin, string end)
        {
            DateTime startDateTime = DateTime.ParseExact(begin, format, CultureInfo.InvariantCulture);
            DateTime endDateTime = DateTime.ParseExact(end, format, CultureInfo.InvariantCulture);
            if (startDateTime >= endDateTime)
            {
                throw new ArgumentException($"Параметр {nameof(begin)}:{begin} должен быть меньше {nameof(end)}:{end}");
            }
            return orderRepository.FilterOrdersByDistrictAndTime(district, startDateTime, endDateTime);
        }

    }
}
