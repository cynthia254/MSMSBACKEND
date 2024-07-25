using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Roles
{
    public class Permissions
    {
        [Key]
        public int ClaimID { get; set; }
         public string ClaimName { get; set; }
        public string LoggedInUser { get; set; }
        public DateTime DateAdded { get; set; }= DateTime.UtcNow;
         






    }
}
