using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Roles
{
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public List<Permissions> Permissions { get; set; }
    }
}
