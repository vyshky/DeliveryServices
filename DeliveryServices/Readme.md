# На проекте использую библиотеки:
> Microsoft.Extensions.DependencyInjection<br/>
> Microsoft.Extensions.Hosting<br/>
> Microsoft.Extensions.Options<br/>
> NLog<br/>
> NLog.Extensions.Logging<br/>
> NLog.Web.AspNetCore<br/>
> NLog.Extensions.Hosting<br/>
> Newtonsoft.Json<br/>

# Аргументы при запуске приложения. Если не передать аргументы при запуске, то настройки подтянутся из appsettings.json. Пример:
> --Settings:cityDistrict=Обжорск<br/>
> --Settings:beginDate="2018-12-21 03:18:44"<br/>
> --Settings:endDate="2018-12-21 03:18:44"<br/>
> --Settings:deliveryLog="log_22.10.2024.txt"<br/>
> --Settings:deliveryOrder="Orders.json"<br/>

# Настройки:
> nlog.config - настроки логирования<br/>
> Ordes.json - тестовые данные<br/>
> appSettings.json - настройки для хранения аргументов с дефолтными значениями<br/>