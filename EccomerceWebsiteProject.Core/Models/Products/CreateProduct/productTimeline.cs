using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Products.CreateProduct
{
    public class productTimeline
    {
        [Key]
        public int Id { get; set; }
        public int ProductID { get; set; } = 0;
        public string ProductName { get; set; } = "None";
        public string ProductDescription { get; set; } = "None";
        public byte[] ImageUpload { get; set; } = new byte[0];
        public int StockQuantity { get; set; } = 0;
        public DateTime DateAdded { get; set; }= DateTime.Now;
        public string AddedBy { get; set; } = "None";
        public int QuantityUpdated { get; set; } = 0;
        public string UpdatedBy { get; set; } = "None";
        public DateTime DateUpdated { get; set; }=DateTime.Now;
        public int StoreId { get; set; } = 0;
        public DateTime DateAttached { get; set; } = DateTime.Now;
        public string AttachedBy { get; set; } = "None";
        public int QuantityAttached { get; set; } = 0;
    }
}
