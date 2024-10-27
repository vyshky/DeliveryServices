namespace DeliveryServices.Services
{
    public interface IServiceOrder
    {
        public void FilterOrdersWithinTimeRange();
        public void FilterOrdersWithinTimeRangeAsync(string district, string beginTime, int endTime = 30);
        public void PrintSettings();
    }
}
