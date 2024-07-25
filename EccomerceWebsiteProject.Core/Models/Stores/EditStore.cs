using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Stores
{
    public class EditStore
    {
        [Key]
        public int EditStoreID { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Website { get; set; } = "None";
        public string PhoneNumber { get; set; } = "None";
        public string Location { get; set; } = "None";
        public string Email { get; set; } = "None";
        public Guid MerchantID { get; set; }
        public DateTime DateUpdated { get; set; }=DateTime.Now;
        public string LoggedInUser { get; set; }= "None";
        public string StoreStatus { get; set; } = "Not Archived";


    }
}
