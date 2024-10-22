using DeliveryServices.Infrastructure.DataAccess;
using DeliveryServices.Service;
using DeliveryServices.Services.Dto;

namespace DeliveryServices.Application
{
    internal class Program
    {
        static string district;
        static string beginDate;
        static string endDate;
        static string formatDate = "yyyy-MM-dd HH:mm:ss";
        static IOrderService orderService;
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 0)
                {
                    district = GetArgument(args, "-cityDistrict");
                    beginDate = GetArgument(args, "-beginDate");
                    endDate = GetArgument(args, "-endDate");
                    CreateServices();
                    List<OrderDto> orders = orderService.FilterOrdersByDistrictAndTime(district, formatDate, beginDate, endDate);
                    Log(orders);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Файл не найден: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        static internal void CreateServices()
        {
            orderService = new OrderService(new OrderRepositoryFile("Orders.json"));

        }

        static internal string GetArgument(string[] args, string key)
        {
            int index = Array.IndexOf(args, key);
            if (index >= 0 && index + 1 < args.Length)
            {
                return args[index + 1];
            }
            throw new ArgumentException($"Аргумент не найден: {key}"); // TODO :: разобраться как называются аргументы и параметры которые передаются в консоли
        }
        static void Log(List<OrderDto> orders)
        {
            foreach (OrderDto order in orders)
            {
                Console.WriteLine($"Id: {order.Id}\n" +
                    $"District: {order.District}\n" +
                    $"Weight: {order.Weight}\n" +
                    $"DeliveryTime: {order.DeliveryTime}\n");
            }
        }
    }
}