using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SvenePrøveProjekt.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
        public int PhoneNr { get; set; } = 0;
        public string Address { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Order?> order { get; set; } = new List<Order?>(); // en til mange relation mellem Customers til Order

    }
}
