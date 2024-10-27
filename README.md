# На проекте использую библиотеки:
> Microsoft.Extensions.Configuration.Json<br/>
> Microsoft.Extensions.DependencyInjection<br/>
> Newtonsoft.Json<br/>
> NLog.Extensions.Logging<br/>
> System.CommandLine<br/>

# Передача неправильных аргументов в консоль, приведет к логированию ошибки в консоль и применит настройки из appSettings.json
# Пример:
> --cityDistrict Обжорск</br>
> --beginDate "2016-10-30 12:57:42"</br>
> --rangeMinutes 30</br>
> --deliveryLog logsConsole/log.log</br>
> --deliveryOrder result/orders.json</br>

# Настройки:
> orders.json - тестовые данные<br/>
> appSettings.json - настройки для хранения аргументов с дефолтными значениями, также в этом файле настраивается nlog<br/>

# Команда на запуск приложения
> dotnet run --cityDistrict "Обжорск" --beginDate "2016-10-30 12:57:42" --rangeMinutes 30 --deliveryLog "/logs/deliverySetting.log" --deliveryOrder "/orders/found_orders.json"<br/>
