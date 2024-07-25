using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Stores
{
    public class StoreProductAssociations
    {
        [Key]
        public int StoreProductID { get; set; }
        public int StoreId { get; set; }
        public int  Quantity{get;set;}
        public int ProductId { get; set; }
     public Guid MerchantID { get; set; }
        public string MerchantName { get; set; } = "None";
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductType { get; set; } = "None";
        public string Amount { get; set; }= "None";
        public string ReorderLevel { get; set; }= "None";
        public string UnitPrice { get; set; } = "None";
        public string Currency { get; set; } = "None";
        public decimal Price { get; set; }= decimal.Zero;
        public int CategoryId { get; set; }=0;
        public byte[] ImageUpload { get; set; }=new byte[0];
        public DateTime UpdatedTime { get; set; }=DateTime.Now;
        public string UpdatedBy { get; set; } = "None";
        public string Status { get; set; } = "Out";
        public string StatusDescription { get; set; } = "None";
        



    }
}
