# На проекте использую библиотеки:
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Hosting
dotnet add package Microsoft.Extensions.Options
dotnet add package NLog.Extensions.Logging
dotnet add package NLog.Web.AspNetCore
dotnet add package NLog.Extensions.Hosting
dotnet add package Newtonsoft.Json

# Аргументы при запуске приложения, пример:
Настройки передаем через консоль при запуске:
--Settings:cityDistrict=Обжорск
--Settings:beginDate="2018-12-21 03:18:44" 
--Settings:endDate="2018-12-21 03:18:44"
--Settings:deliveryLog="log_22.10.2024.txt"
--Settings:deliveryOrder="Orders.json"

# Настройки:
nlog.config - настроки логирования
Ordes.json - тестовые данные
appSettings.json - настройки для хранения аргументов с дефолтными значениями