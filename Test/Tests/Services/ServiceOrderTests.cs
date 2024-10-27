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
            // Настройка mock-объектов
            _mockLogger = new Mock<ILogger<ServiceOrder>>();
            _mockOptions = new Mock<IOptions<Settings>>();

            // Настройка тестовых значений для Settings
            _settings = new Settings
            {
                CityDistrict = "Тестовый район",
                BeginDate = "2023-10-01 10:00:00",
                RangeMinutes = 30,
                DeliveryOrder = "test_output.json",
                DeliveryLog = "test_log.txt"
            };

            // Привязка mock-объекта Options к настройкам
            _mockOptions.Setup(opt => opt.Value).Returns(_settings);

            // Создание экземпляра ServiceOrder с mock-объектами
            _serviceOrder = new ServiceOrder(_mockOptions.Object, _mockLogger.Object);
        }

        [Fact]
        public void FilterOrdersWithinTimeRange_ShouldFilterOrdersCorrectly()
        {
            // Подготовка тестовых данных (список заказов)
            var orders = new List<Delivery>
            {
                new Delivery { Id = "1", District = "Тестовый район", DeliveryTime = DateTime.Parse("2023-10-01 10:05:00") },
                new Delivery { Id = "2", District = "Другой район", DeliveryTime = DateTime.Parse("2023-10-01 10:10:00") },
                new Delivery { Id = "3", District = "Тестовый район", DeliveryTime = DateTime.Parse("2023-10-01 10:20:00") }
            };

            // Сериализация в JSON и запись в файл, чтобы использовать его в методе
            File.WriteAllText("orders.json", JsonConvert.SerializeObject(orders));

            // Выполнение метода
            var result = _serviceOrder.FilterOrdersWithinTimeRange(_settings.CityDistrict, _settings.BeginDate);

            // Проверка результата
            Assert.Equal(2, result.Count);
            Assert.Contains(result, o => o.Id == "1");
            Assert.Contains(result, o => o.Id == "3");
        }

        [Fact]
        public async Task FilterOrdersWithinTimeRangeAndSaveToFileAsync_ShouldSaveFilteredOrders()
        {
            // Подготовка тестовых данных (список заказов)
            var orders = new List<Delivery>
            {
                new Delivery { Id = "1", District = "Тестовый район", DeliveryTime = DateTime.Parse("2023-10-01 10:05:00") },
                new Delivery { Id = "2", District = "Другой район", DeliveryTime = DateTime.Parse("2023-10-01 10:10:00") },
                new Delivery { Id = "3", District = "Тестовый район", DeliveryTime = DateTime.Parse("2023-10-01 10:20:00") }
            };

            // Сериализация в JSON и запись в файл, чтобы использовать его в методе
            File.WriteAllText("orders.json", JsonConvert.SerializeObject(orders));

            // Выполнение метода
            await _serviceOrder.FilterOrdersWithinTimeRangeAndSaveToFileAsync();

            // Проверка, что файл был создан и содержит ожидаемый результат
            Assert.True(File.Exists(_settings.DeliveryOrder));
            var filteredJson = File.ReadAllText(_settings.DeliveryOrder);
            var filteredOrders = JsonConvert.DeserializeObject<List<Delivery>>(filteredJson);

            // Проверка содержания отфильтрованных заказов
            Assert.Equal(2, filteredOrders.Count);
            Assert.Contains(filteredOrders, o => o.Id == "1");
            Assert.Contains(filteredOrders, o => o.Id == "3");

            // Очистка тестовых файлов после выполнения теста
            File.Delete("orders.json");
            File.Delete(_settings.DeliveryOrder);
        }

    }
}
