using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Orders
{
    public class NewOrder
    {
        [Key]
        public int OrderID { get; set; } // Assuming this is the primary key in the database

        public int ShiftId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductAmount { get; set; }
        public string OrderNo { get; set; } // Unique identifier for the order
        public string Status { get; set; } = "Pending Payment"; // Default status
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string OrderMadeBy { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }

        public NewOrder()
        {
            OrderNo = Guid.NewGuid().ToString(); // Generate a unique identifier for OrderNo
        }
    }
}
