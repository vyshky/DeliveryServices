using DeliveryServices.Models;
using DeliveryServices.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace DeliveryServices.Application
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                await RunApplicationAsync(args);
            }
        }

        static internal async Task RunApplicationAsync(string[] args)
        {
            // Early init of NLog to allow startup and exception logging, before host is built
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            // Создание и настройка хоста
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
                    // Добавляем аргументы командной строки в конфигурацию
                    config.AddCommandLine(args);
                })
                .ConfigureServices((context, services) =>
                {
                    // Привязка конфигурации к классу Settings
                    services.Configure<Settings>(context.Configuration.GetSection("Settings")); ;
                    // Регистрация сервиса
                    services.AddTransient<IOrderService, OrderService>();
                    // Валидация настроек по аннотациям
                    services.AddOptions<Settings>().ValidateDataAnnotations().ValidateOnStart();
                })
                .ConfigureLogging(logging =>
                {
                    // Удаляем стандартных провайдеров логирования
                    logging.ClearProviders();
                })
                .UseNLog()  // Подключаем NLog как провайдер логирования
                .Build();

            // Получение сервиса и вызов метода
            var myService = host.Services.GetRequiredService<IOrderService>();
            myService.PrintSettings();

            await host.RunAsync();


            // TODO :: Логика. Сделать ожидание, для ввода с клавиатуры
        }
    }
}