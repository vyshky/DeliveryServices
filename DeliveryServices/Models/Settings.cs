using System.ComponentModel.DataAnnotations;

namespace DeliveryServices.Models
{
    public class Settings
    {
        [Required]
        public string CityDistrict { get; set; }
        [DateFormat("yyyy-MM-dd HH:mm:ss")]
        [Required]
        public string BeginDate { get; set; }
        [DateFormat("yyyy-MM-dd HH:mm:ss")]
        [Required]
        public string EndDate { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Максимальная длина строки 100 символов.")]
        public string DeliveryLog { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Максимальная длина строки 100 символов.")]
        public string DeliveryOrder { get; set; }
    }
}
