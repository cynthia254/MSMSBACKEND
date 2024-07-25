using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.PlatformUsers
{
    public class Creation_UserMerchant
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string RoleName { get; set; }
        public string LoggedInUser { get; set; }
        public DateTime DateAdded { get; set; }=DateTime.Now;
        public string Status { get; set; } = "Deactivated";
        public Guid MerchantID { get; set; }
    }
}
