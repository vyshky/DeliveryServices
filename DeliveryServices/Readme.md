# На проекте использую библиотеки:
> Microsoft.Extensions.DependencyInjection<br/>
> Microsoft.Extensions.Options<br/>
> NLog.Extensions.Logging<br/>
> Newtonsoft.Json<br/>
> System.CommandLine<br/>

# Передача неправильных аргументов в консоль, приведет логированию ошибки и подтянет настройки из appSettings.json
# Пример:
> --cityDistrict Обжорск<br/>
> --beginDate "2000-12-2 10:10:00"<br/>
> --endDate "2024-12-2 10:10:00"<br/>
> --deliveryOrder logs/log.log<br/>
> --deliveryLog result/orders.json<br/>

# Настройки:
> Ordes.json - тестовые данные<br/>
> appSettings.json - настройки для хранения аргументов с дефолтными значениями, также в этом файле настраивается nlog<br/>

# Команда на запуск приложения
> dotnet run --cityDistrict "Обжорск" --beginDate "2024-01-01 08:00:00" --endDate "2024-12-31 08:00:00" --deliveryLog "/logs/deliverySetting.log" --deliveryOrder "/orders/found_orders.json"<br/>
