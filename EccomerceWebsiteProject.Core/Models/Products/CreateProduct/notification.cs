using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Products.CreateProduct
{
    public class notification
    {
        [Key]
        public int NotificationID { get; set; }
        public string Message { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.Now;
    }
}
