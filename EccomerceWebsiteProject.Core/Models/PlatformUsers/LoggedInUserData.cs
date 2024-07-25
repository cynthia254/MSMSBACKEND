namespace EccomerceWebsiteProject.Core.Models.PlatformUsers
{
    public class LoggedInUserData
    {
      
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }
        
        
    }
}
