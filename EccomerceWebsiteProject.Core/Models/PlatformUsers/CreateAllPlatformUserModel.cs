using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.PlatformUsers
{
    public class CreateAllPlatformUserModel : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAdress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; } = "Admin";
        
    }
}
