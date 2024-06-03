using System.ComponentModel.DataAnnotations;

namespace SvenePrøveProjekt.Models
{
    public class City
    {
        [Key]
        public int CityID { get; set; } = 0;
        public string CityName { get; set; } = string.Empty;
        public int ZIPCode { get; set; } = 0; 

    }
}
