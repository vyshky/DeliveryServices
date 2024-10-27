using DeliveryServices.Models;
using DeliveryServices.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;

namespace DeliveryServices.Tests.Services
{
    public class ServiceOrderTests
    {

        private readonly Mock<ILogger<ServiceOrder>> _mockLogger;
        private readonly Mock<IOptions<Settings>> _mockOptions;
        private readonly ServiceOrder _serviceOrder;
        private readonly Settings _settings;

        public ServiceOrderTests()
        {
            _mockLogger = new Mock<ILogger<ServiceOrder>>();
            _mockOptions = new Mock<IOptions<Settings>>();

            _settings = new Settings
            {
                CityDistrict = "Тестовый район",
                BeginDate = "2023-10-01 10:00:00",
                RangeMinutes = 30,
                DeliveryOrder = "test_output.json",
                DeliveryLog = "test_log.txt"
            };

            _mockOptions.Setup(opt => opt.Value).Returns(_settings);

            _serviceOrder = new ServiceOrder(_mockOptions.Object, _mockLogger.Object);
        }

        [Fact]
        public void FilterOrdersWithinTimeRange_ShouldFilterOrdersCorrectly()
        {
            var orders = new List<Delivery>
            {
                new Delivery { Id = "1", District = "Тестовый район", DeliveryTime = DateTime.Parse("2023-10-01 10:05:00") },
                new Delivery { Id = "2", District = "Другой район", DeliveryTime = DateTime.Parse("2023-10-01 10:10:00") },
                new Delivery { Id = "3", District = "Тестовый район", DeliveryTime = DateTime.Parse("2023-10-01 10:20:00") }
            };

            File.WriteAllText("orders.json", JsonConvert.SerializeObject(orders));

            var result = _serviceOrder.FilterOrdersWithinTimeRange(_settings.CityDistrict, _settings.BeginDate);

            Assert.Equal(2, result.Count);
            Assert.Contains(result, o => o.Id == "1");
            Assert.Contains(result, o => o.Id == "3");
        }

        [Fact]
        public async Task FilterOrdersWithinTimeRangeAndSaveToFileAsync_ShouldSaveFilteredOrders()
        {
            var orders = new List<Delivery>
            {
                new Delivery { Id = "1", District = "Тестовый район", DeliveryTime = DateTime.Parse("2023-10-01 10:05:00") },
                new Delivery { Id = "2", District = "Другой район", DeliveryTime = DateTime.Parse("2023-10-01 10:10:00") },
                new Delivery { Id = "3", District = "Тестовый район", DeliveryTime = DateTime.Parse("2023-10-01 10:20:00") }
            };

            File.WriteAllText("orders.json", JsonConvert.SerializeObject(orders));

            await _serviceOrder.FilterOrdersWithinTimeRangeAndSaveToFileAsync();

            Assert.True(File.Exists(_settings.DeliveryOrder));
            var filteredJson = File.ReadAllText(_settings.DeliveryOrder);
            var filteredOrders = JsonConvert.DeserializeObject<List<Delivery>>(filteredJson);

            Assert.Equal(2, filteredOrders.Count);
            Assert.Contains(filteredOrders, o => o.Id == "1");
            Assert.Contains(filteredOrders, o => o.Id == "3");

            File.Delete("orders.json");
            File.Delete(_settings.DeliveryOrder);
        }

    }
}
