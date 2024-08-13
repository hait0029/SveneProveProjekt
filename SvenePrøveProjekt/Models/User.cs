using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SvenePrøveProjekt.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; } = 0;
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
        public int PhoneNr { get; set; } = 0;
        public string Address { get; set; } = string.Empty;
        public int? LoginId { get; set; } // Foreign key property
        public Login? login { get; set; }// 1 til 1 relation aka Navigation property
        public int? CityId { get; set; } // Foreign key property

        public City? cities { get; set; }// en til en relation mellem User til City

        [JsonIgnore]
        public Role? RoleType { get; set; }// Navigation property

        [JsonIgnore]
        public List<Order?> order { get; set; } = new List<Order?>(); // en til mange relation mellem Customers til Order

    }
}
