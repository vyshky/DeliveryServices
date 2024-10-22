using DeliveryServices.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DeliveryServices.Services
{
    public class OrderService : IOrderService
    {
        private readonly Settings settings;
        private readonly ILogger logger;

        public OrderService(IOptions<Settings> options, ILogger<OrderService> logger)
        {
            this.settings = options.Value;
            this.logger = logger;
        }

        public void PrintSettings()
        {
            logger.LogInformation($"Вывод всех настроек на консоль");
            Console.WriteLine($"District: {settings.CityDistrict}");
            Console.WriteLine($"Begin: {settings.BeginDate}");
            Console.WriteLine($"End: {settings.EndDate}");
            Console.WriteLine($"DeliveryLog: {settings.DeliveryLog}");
            Console.WriteLine($"DeliveryOrder: {settings.DeliveryOrder}");
        }
    }
}
