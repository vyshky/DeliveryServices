using DeliveryServices.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;

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
        public void FilterOrdersWithinTimeRange() {            
            FilterOrdersWithinTimeRangeAsync(settings.CityDistrict, settings.BeginDate);
        }
        public async void FilterOrdersWithinTimeRangeAsync(string district, string beginTime, int endTime = 30)
        {
            logger.LogInformation($"Вызванна функция FilterOrdersWithinTimeRange(district={district}, beginTime={beginTime}, endTime={endTime})");

            using (StreamReader reader = new StreamReader("orders.json"))
            {
                string line = await reader.ReadToEndAsync();

            }
            //toDO ::
            // залогировать в файл settings.DeliveryLog
            // записать отфильрованные ордера в файл settings.DeliveryOrder
        }

        public void PrintSettings()
        {
            logger.LogInformation($"Вывод всех настроек на консоль");
            logger.LogInformation($"District: {settings.CityDistrict}");
            logger.LogInformation($"Begin: {settings.BeginDate}");
            logger.LogInformation($"End: {settings.EndDate}");
            logger.LogInformation($"DeliveryLog: {settings.DeliveryLog}");
            logger.LogInformation($"DeliveryOrder: {settings.DeliveryOrder}");
        }
    }
}
