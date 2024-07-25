namespace EccomerceWebsiteProject.Core.DTOS.PlatformUsers
{
    public class CreateAllUserPlatform
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; } = "Admin";
    }
}
