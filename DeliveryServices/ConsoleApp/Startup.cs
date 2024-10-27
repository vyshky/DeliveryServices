using DeliveryServices.Models;
using DeliveryServices.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using LogLevel = NLog.LogLevel;

namespace DeliveryServices.ConsoleApp
{
    internal class StartUp
    {
        public static ServiceProvider InitializeServices(Settings settings)
        {
            var configuration = CreateConfiguration();
            var services = new ServiceCollection();

            ConfigureApplicationSettings(services, configuration, settings);
            RegisterApplicationServices(services);
            ConfigureLoggingServices(services, configuration, settings.DeliveryLog);
            return services.BuildServiceProvider();
        }

        private static IConfiguration CreateConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        private static void ConfigureApplicationSettings(IServiceCollection services, IConfiguration configuration, Settings settings)
        {
            services.Configure<Settings>(options =>
            {
                if (!string.IsNullOrEmpty(settings.CityDistrict))
                {
                    configuration.GetSection("Settings:CityDistrict").Value = settings.CityDistrict;
                }

                if (!string.IsNullOrEmpty(settings.BeginDate))
                {
                    configuration.GetSection("Settings:BeginDate").Value = settings.BeginDate;
                }

                if (!string.IsNullOrEmpty(settings.EndDate))
                {
                    configuration.GetSection("Settings:EndDate").Value = settings.EndDate;
                }

                if (!string.IsNullOrEmpty(settings.DeliveryLog))
                {
                    configuration.GetSection("Settings:DeliveryLog").Value = settings.DeliveryLog;
                }

                if (!string.IsNullOrEmpty(settings.DeliveryOrder))
                {
                    configuration.GetSection("Settings:DeliveryOrder").Value = settings.DeliveryOrder;
                }

                options.CityDistrict = configuration.GetSection("Settings:CityDistrict").Value;
                options.BeginDate = configuration.GetSection("Settings:BeginDate").Value;
                options.EndDate = configuration.GetSection("Settings:EndDate").Value;
                options.DeliveryLog = configuration.GetSection("Settings:DeliveryLog").Value;
                options.DeliveryOrder = configuration.GetSection("Settings:DeliveryOrder").Value;
            });
        }

        private static void ConfigureLoggingServices(IServiceCollection services, IConfiguration configuration, string path = null)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog(configuration);
            });

            if (!string.IsNullOrEmpty(path))
            {
                LogManager.Configuration = new NLogLoggingConfiguration(configuration.GetSection("NLog"));

                var target = LogManager.Configuration.FindTargetByName<FileTarget>("file");

                if (target != null)
                {
                    target.FileName = Path.Combine(AppContext.BaseDirectory, path);
                }
                else
                {
                    LoggingRule fileLogRule = LogManager.Configuration.LoggingRules
                        .Where(rule =>
                                rule.Targets
                        .Any(t => t is FileTarget fileTarget && fileTarget.Name == "file"))
                        .ToList().First();
                    if (fileLogRule != null)
                    {
                        LogManager.Configuration.LoggingRules.Remove(fileLogRule);
                    }

                    // Добавляем путь для логирования
                    target = new FileTarget("file");
                    target.FileName = Path.Combine(AppContext.BaseDirectory, path);
                    target.Layout = "${longdate} ${level} ${message}";
                    LogManager.Configuration.AddTarget(target);

                    var rule = new LoggingRule("*", LogLevel.Debug, LogLevel.Fatal, target);
                    LogManager.Configuration.LoggingRules.Add(rule);
                }

                LogManager.ReconfigExistingLoggers();
            }
        }

        private static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddSingleton<IServiceOrder, ServiceOrder>();
        }
    }
}
