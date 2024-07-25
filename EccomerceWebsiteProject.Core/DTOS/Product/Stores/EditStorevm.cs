using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.DTOS.Product.Stores
{
    public class EditStorevm
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Website { get; set; } = "None";
        public string PhoneNumber { get; set; } = "None";
        public string Location { get; set; } = "None";
        public string Email { get; set; } = "None";
        public Guid MerchantID { get; set; }
    }
}
