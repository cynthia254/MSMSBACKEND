using System.ComponentModel.DataAnnotations;
using System.Security;

namespace EccomerceWebsiteProject.Core.Models.Roles
{
    public class AddRole
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public List<Permissions> Permissions { get; set; }
    }
}
