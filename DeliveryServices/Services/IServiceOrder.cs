using DeliveryServices.Models;

namespace DeliveryServices.Services
{
    public interface IServiceOrder
    {
         Task FilterOrdersWithinTimeRangeAndSaveToFileAsync();
         List<Delivery> FilterOrdersWithinTimeRange(string district, string beginTime, int rangeMinutes);
         void PrintSettings();
    }
}
