using DeliveryServices.Domain.Repository;
using DeliveryServices.Services.Dto;
using Newtonsoft.Json;

namespace DeliveryServices.Infrastructure.DataAccess
{
    internal class OrderRepositoryFile : IOrderRepository
    {

        List<OrderDto> orders;
        public OrderRepositoryFile(string path)
        {
            string json;
            if (File.Exists(path))
            {
                using (StreamReader reader = File.OpenText(path))
                {
                    orders = JsonConvert.DeserializeObject<List<OrderDto>>(reader.ReadToEnd());
                }
            }
            else
            {
                throw new FileNotFoundException(path);
            }
        }

        public List<OrderDto> FilterOrdersByDistrictAndTime(string district, DateTime begin, DateTime end)
        {
            return orders
                .Where(order => order.District.Equals(district, StringComparison.OrdinalIgnoreCase) &&
                                order.DeliveryTime >= begin &&
                                order.DeliveryTime <= end)
                .ToList();
        }
    }
}
