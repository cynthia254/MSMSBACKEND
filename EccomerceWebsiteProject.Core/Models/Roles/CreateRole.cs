using System.ComponentModel.DataAnnotations;
using System.Security;

namespace EccomerceWebsiteProject.Core.Models.Roles
{
    public class CreateRole
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public ICollection<Permissions> Permissions { get; set; }
    }
}
