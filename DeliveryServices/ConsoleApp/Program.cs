﻿using DeliveryServices.Models;
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
            if (!(args.Length > 0))
            {
                return;
            }
            else
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
                    // Добавляем аргументы командной строки в конфигурацию
                    config.AddCommandLine(args);
                })
                .ConfigureServices((context, services) =>
                {
                    // Регистрация сервиса
                    services.AddTransient<IOrderService, OrderService>();

                    // Привязка конфигурации к классу Settings
                    services.Configure<Settings>(context.Configuration.GetSection("Settings"));
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


            // TODO :: сделать ожидание, для ввода с клавиатуры
        }
    }
}