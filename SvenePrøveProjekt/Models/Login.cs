using System.Text.Json.Serialization;

namespace SvenePrøveProjekt.Models
{
    public class Login
    {
        [Key]
        public int LoginID { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public int? RoleId { get; set; } // Foreign key property
        public Role? RoleType { get; set; }  // Navigation property

    [JsonIgnore]
    //1-1 FK between Login and User.
    public User? Users { get; set; }
    }
}
