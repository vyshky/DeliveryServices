using DeliveryServices.Models;
using DeliveryServices.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

namespace DeliveryServices.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
 
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                logger.Debug("Инициализация Main");
                var host = CreateHostBuilder(args).Build();
                var orderService = host.Services.GetRequiredService<IOrderService>();
                orderService.PrintSettings();
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Приложение остановлено из-за ошибки");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        private static void ConfigureNLog(string logFilePath)
        {
            var config = new NLog.Config.LoggingConfiguration();

            var fileTarget = new NLog.Targets.FileTarget("logfile")
            {
                FileName = logFilePath,
                Layout = "${longdate} ${uppercase:${level}} ${message} ${exception}"
            };

            config.AddTarget(fileTarget);
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, fileTarget);

            LogManager.Configuration = config;
        }

        private static Dictionary<string, string> ParseArgs(string[] args)
        {
            var arguments = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                if (arg.StartsWith("_"))
                {
                    var keyValue = arg.Substring(1).Split(new[] { '=' }, 2);
                    if (keyValue.Length == 2)
                    {
                        arguments[keyValue[0].ToLower()] = keyValue[1].Trim('"', ' ');
                    }
                }
            }
            return arguments;
        }

        static internal IHostBuilder CreateHostBuilder(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                 .AddJsonFile(path: "appSettings.json").Build();
            NLog.Extensions.Logging.ConfigSettingLayoutRenderer.DefaultConfiguration = config;
            IHostBuilder host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.Configure<Settings>(context.Configuration.GetSection("Settings"));
                    services.AddSingleton<IOrderService, OrderService>();
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
                    config.AddCommandLine(args);
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .UseNLog();
            return host;
        }
    }
}
