using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Roles
{
    public class RolePermission
    {
        [Key]
        public int RolePermissionID { get; set; }
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }    
        public string PermissionName
        { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
