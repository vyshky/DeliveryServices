using DeliveryServices.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;

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
        public async Task FilterOrdersWithinTimeRangeAndSaveToFileAsync()
        {
            logger.LogInformation($"Вызванна функция FilterOrdersWithinTimeRangeAndSaveToFileAsync()");

            List<Delivery> filteredDeliveries = FilterOrdersWithinTimeRange(settings.CityDistrict, settings.BeginDate);

            string filteredJson = JsonConvert.SerializeObject(filteredDeliveries, Formatting.Indented);

            string directoryPath = Path.GetDirectoryName(settings.DeliveryOrder);
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            await File.WriteAllTextAsync(settings.DeliveryOrder, filteredJson);
        }
        public List<Delivery> FilterOrdersWithinTimeRange(string district, string beginTime, int rangeMinutes = 30)
        {
            logger.LogInformation($"Вызванна функция FilterOrdersWithinTimeRange(district={district}, beginTime={beginTime}, endTime={rangeMinutes})");

            string json = File.ReadAllText("orders.json");

            List<Delivery> deliveries = JsonConvert.DeserializeObject<List<Delivery>>(json);
            DateTime beginDateTime = DateTime.ParseExact(beginTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            DateTime endDateTime = beginDateTime.AddMinutes(settings.RangeMinutes > 0 ? settings.RangeMinutes : rangeMinutes);

            return deliveries
             .Where(x => x.District.Equals(district, StringComparison.OrdinalIgnoreCase) && x.DeliveryTime >= beginDateTime && x.DeliveryTime <= endDateTime)
             .ToList();
        }

        public void PrintSettings()
        {
            logger.LogInformation($"Вывод всех настроек на консоль");
            logger.LogInformation($"District: {settings.CityDistrict}");
            logger.LogInformation($"Begin: {settings.BeginDate}");
            logger.LogInformation($"End: {settings.RangeMinutes}");
            logger.LogInformation($"DeliveryLog: {settings.DeliveryLog}");
            logger.LogInformation($"DeliveryOrder: {settings.DeliveryOrder}");
        }
    }
}
