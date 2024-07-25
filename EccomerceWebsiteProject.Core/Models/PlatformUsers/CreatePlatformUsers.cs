using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.PlatformUsers
{
    public class CreatePlatformUsers
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; } = "Admin";
    }
}
