using DeliveryServices.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;



namespace DeliveryServices.Services
{
    public class OrderService : IOrderService
    {
        private readonly Settings settings;
        private readonly ILogger<OrderService> logger;

        public OrderService(IOptions<Settings> options, ILogger<OrderService> logger)
        {
            this.settings = options.Value;
            this.logger = logger;
        }

        public void PrintSettings()
        {
            logger.LogError($"Вывод всех настроек на консоль");
            logger.LogInformation($"District: {settings.CityDistrict}");
            logger.LogInformation($"Begin: {settings.BeginDate}");
            logger.LogInformation($"End: {settings.EndDate}");
            logger.LogInformation($"DeliveryLog: {settings.DeliveryLog}");
            logger.LogInformation($"DeliveryOrder: {settings.DeliveryOrder}");
        }
    }
}
