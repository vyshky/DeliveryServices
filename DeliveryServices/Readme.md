# �� ������� ��������� ����������:
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Hosting
dotnet add package Microsoft.Extensions.Options
dotnet add package NLog.Extensions.Logging
dotnet add package NLog.Web.AspNetCore
dotnet add package NLog.Extensions.Hosting
dotnet add package Newtonsoft.Json

# ��������� ��� ������� ����������, ������:
��������� �������� ����� ������� ��� �������:
--Settings:cityDistrict=�������
--Settings:beginDate="2018-12-21 03:18:44" 
--Settings:endDate="2018-12-21 03:18:44"
--Settings:deliveryLog="log_22.10.2024.txt"
--Settings:deliveryOrder="Orders.json"

# ���������:
nlog.config - �������� �����������
Ordes.json - �������� ������
appSettings.json - ��������� ��� �������� ���������� � ���������� ����������