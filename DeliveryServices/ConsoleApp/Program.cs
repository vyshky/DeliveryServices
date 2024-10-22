using DeliveryServices.Models;
using DeliveryServices.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DeliveryServices.Application
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (!(args.Length > 0))
            {
                return;
            }
            else
            {
                ConfigureServices(args);
            }
        }

        static public IConfiguration AppConfiguration { get; set; }
        static internal async Task ConfigureServices(string[] args)
        {
            // Создание и настройка хоста
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Добавляем аргументы командной строки в конфигурацию
                    config.AddCommandLine(args);
                })
                .ConfigureServices((context, services) =>
                {
                    // Регистрация сервиса
                    services.AddSingleton<IOrderService, OrderService>();

                    // Привязка конфигурации к классу Settings
                    services.Configure<Settings>(context.Configuration.GetSection("Settings"));
                })
                .Build();

            // Получение сервиса и вызов метода
            var myService = host.Services.GetRequiredService<IOrderService>();
            myService.PrintSettings();

            await host.RunAsync();
        }

        static internal string GetArgument(string[] args, string key)
        {
            int index = Array.IndexOf(args, key);
            if (index >= 0 && index + 1 < args.Length)
            {
                return args[index + 1];
            }
            throw new ArgumentException($"Аргумент не найден: {key}");
        }
    }
}