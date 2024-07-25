using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Orders
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; } // Primary key in the database

        public int ShiftId { get; set; }
        public string Status { get; set; } = "Pending Payment";
        public string OrderNo { get; set; } // Unique identifier for the order
        public string OrderMadeBy { get; set; } // User who made the order
        public DateTime CreatedDate { get; set; } // Date and time when the order was created

        // Navigation property for products
        public virtual ICollection<ProductData> Products { get; set; }

        public Order()
        {
            OrderNo = Guid.NewGuid().ToString(); // Generate a unique identifier for OrderNo
        }

    }
}

