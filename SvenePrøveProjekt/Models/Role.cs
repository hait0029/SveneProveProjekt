using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SvenePrøveProjekt.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; } = 0;
        public string RoleType { get; set; } = string.Empty;


        [JsonIgnore]
        public List<Login?> logins { get; set; } = new List<Login?>();// en til mange relation mellem Role til Login
    }
}
