using DeliveryServices.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata.Ecma335;



namespace DeliveryServices.Services
{
    public class ServiceOrder : IServiceOrder
    {
        private readonly Settings settings;
        private readonly ILogger<ServiceOrder> logger;

        public ServiceOrder(IOptions<Settings> options, ILogger<ServiceOrder> logger)
        {
            this.settings = options.Value;
            this.logger = logger;
        }

        public void FilterOrdersWithinTimeRange(string district, string beginTime, int endTime = 30)
        {
            //toDO ::
            // залогировать в файл settings.DeliveryLog
            // записать отфильрованные ордера в файл settings.DeliveryOrder
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
