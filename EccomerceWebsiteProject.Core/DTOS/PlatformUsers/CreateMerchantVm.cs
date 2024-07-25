namespace EccomerceWebsiteProject.Core.DTOS.PlatformUsers
{
    public class CreateMerchantVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }
        public string CompanyWebsite { get; set; } = "None";
        public string Status { get; set; } = "New";
        public string Role { get; set; } = "Supplier";
        public bool IsConfirmed { get; set; } = false;



    }
}
