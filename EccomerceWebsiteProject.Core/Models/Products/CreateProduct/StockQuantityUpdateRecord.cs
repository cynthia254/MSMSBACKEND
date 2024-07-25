using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Products.CreateProduct
{
    public class StockQuantityUpdateRecord
    {
        [Key]
     
            public int UpdateQuantityID { get; set; }
            public int ProductId { get; set; }
            public int QuantityUpdated { get; set; }
            public DateTime UpdateTime { get; set; }=DateTime.Now;
            public string UpdatedBy { get; set; }
            public string ProductName { get; set; }
        

    }
}
