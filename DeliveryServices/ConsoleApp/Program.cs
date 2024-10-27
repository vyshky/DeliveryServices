using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using DeliveryServices.Services;
using DeliveryServices.ConsoleApp;
using DeliveryServices.Models;
using NLog;

namespace DeliveryServices.Application
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Settings settings = await ValidateCmdAsync(args);
            ServiceProvider serviceProvider = StartUp.InitializeServices(settings);
            var serviceOrder = serviceProvider.GetRequiredService<IServiceOrder>();
            serviceOrder.FilterOrdersWithinTimeRangeAndSaveToFileAsync();
            LogManager.Shutdown();
        }

        static async Task<Settings> ValidateCmdAsync(string[] args)
        {
            // Опции для командной строки
            var cityDistrictOption = new Option<string>(
                name: "--cityDistrict",
                description: "Район доставки");

            var beginDateOption = new Option<string>(
                name: "--beginDate",
                description: "Дата начала доставки (формат: yyyy-MM-dd HH:mm:ss)");

            var rangeMinutesOption = new Option<string>(
                name: "--rangeMinutes",
                description: "Дата окончания доставки в int");

            var deliveryLogOption = new Option<string>(
                name: "--deliveryLog",
                description: "Путь к файлу с логами");

            var deliveryOrderOption = new Option<string>(
                name: "--deliveryOrder",
                description: "Путь к файлу с результатами заказов");

            // Создание корневой команды
            var rootCommand = new RootCommand("Программа для фильтрации заказов службы доставки");

            // Добавляем опции в команду
            rootCommand.AddOption(cityDistrictOption);
            rootCommand.AddOption(beginDateOption);
            rootCommand.AddOption(rangeMinutesOption);
            rootCommand.AddOption(deliveryLogOption);
            rootCommand.AddOption(deliveryOrderOption);

            // Создаем объект Settings с значениями по умолчанию
            Settings settings = new Settings();

            // Настраиваем обработчик команды
            rootCommand.SetHandler((cityDistrict, beginDate, rangeMinutes, deliveryLog, deliveryOrder) =>
            {
                if (!string.IsNullOrEmpty(cityDistrict))
                    settings.CityDistrict = cityDistrict;
                if (!string.IsNullOrEmpty(beginDate))
                    settings.BeginDate = beginDate;
                if (!string.IsNullOrEmpty(rangeMinutes))
                    settings.RangeMinutes = int.Parse(rangeMinutes);
                if (!string.IsNullOrEmpty(deliveryLog))
                    settings.DeliveryLog = deliveryLog;
                if (!string.IsNullOrEmpty(deliveryOrder))
                    settings.DeliveryOrder = deliveryOrder;
            },
                cityDistrictOption,
                beginDateOption,
                rangeMinutesOption,
                deliveryLogOption,
                deliveryOrderOption);


            // Запускаем команду
            await rootCommand.InvokeAsync(args);

            // Возвращаем объект settings с установленными значениями
            return settings;
        }
    }
}
