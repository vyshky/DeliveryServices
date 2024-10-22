using DeliveryServices.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryServices.Services
{
    public class OrderService : IOrderService
    {
        private readonly Settings _settings;

        public OrderService(IOptions<Settings> options)
        {
            _settings = options.Value;
        }

        public void PrintSettings()
        {
            Console.WriteLine($"District: {_settings.CityDistrict}");
            Console.WriteLine($"Begin: {_settings.BeginDate}");
            Console.WriteLine($"End: {_settings.EndDate}");
            Console.WriteLine($"DeliveryLog: {_settings.DeliveryLog}");
            Console.WriteLine($"DeliveryOrder: {_settings.DeliveryOrder}");
        }
    }
}
