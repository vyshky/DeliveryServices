# На проекте использую библиотеки:
> Microsoft.Extensions.DependencyInjection
> Microsoft.Extensions.Hosting
> Microsoft.Extensions.Options
> NLog.Extensions.Logging
> NLog.Web.AspNetCore
> NLog.Extensions.Hosting
> Newtonsoft.Json

# Аргументы при запуске приложения, пример:
> --Settings:cityDistrict=Обжорск
> --Settings:beginDate="2018-12-21 03:18:44" 
> --Settings:endDate="2018-12-21 03:18:44"
> --Settings:deliveryLog="log_22.10.2024.txt"
> --Settings:deliveryOrder="Orders.json"

# Настройки:
> nlog.config - настроки логирования
> Ordes.json - тестовые данные
> Ordes.json - тестовые данные
> appSettings.json - настройки для хранения аргументов с дефолтными значениями