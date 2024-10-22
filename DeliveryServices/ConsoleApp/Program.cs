namespace DeliveryServices.Application
{
    internal class Program
    {
        static string formatDate = "yyyy-MM-dd HH:mm:ss";
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 0)
                {
                    string district = GetArgument(args, "-cityDistrict");
                    string beginDate = GetArgument(args, "-beginDate");
                    string endDate = GetArgument(args, "-endDate");
                    //List<OrderDto> orders = orderService.FilterOrdersByDistrictAndTime(district, formatDate, beginDate, endDate);
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

        //static internal void CreateServices()
        //{
        //    orderService = new OrderService(new OrderRepositoryFile("Orders.json"));

        //}

        static internal string GetArgument(string[] args, string key)
        {
            int index = Array.IndexOf(args, key);
            if (index >= 0 && index + 1 < args.Length)
            {
                return args[index + 1];
            }
            throw new ArgumentException($"Аргумент не найден: {key}");
        }
        //static void Log(List<OrderDto> orders)
        //{
        //    foreach (OrderDto order in orders)
        //    {
        //        Console.WriteLine($"Id: {order.Id}\n" +
        //            $"District: {order.District}\n" +
        //            $"Weight: {order.Weight}\n" +
        //            $"DeliveryTime: {order.DeliveryTime}\n");
        //    }
        //}
    }
}