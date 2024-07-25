using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EccomerceWebsiteProject.Core.Models.Orders
{
    public class ProductData
    {
        [Key]
        public int ProductID { get; set; } // Primary key in the database

        public int OrderID { get; set; } // Foreign key to OrderData
        public string ProductName { get; set; }
        public decimal ProductAmount { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        [ForeignKey("OrderID")]
        public string Status { get; set; } // Status of the product in the order
        public DateTime CreatedDate { get; set; } // Date and time when the product was added

        // Navigation property to OrderData
        public virtual Order Order { get; set; }
    }
}

