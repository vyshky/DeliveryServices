using System.ComponentModel.DataAnnotations;

namespace DeliveryServices.Models
{
    public class Delivery
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Максимальная длина строки 100 символов.")]
        public string District { get; set; }
        [Required]
        [DateFormat("yyyy-MM-dd HH:mm:ss")]
        public DateTime DeliveryTime { get; set; }
    }

}
