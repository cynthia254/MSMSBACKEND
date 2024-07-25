using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.Orders
{
    public class PaymentData
    {
        [Key]
        public Guid PaymentNumber { get; set; }
        public decimal AmountPaid { get; set; } = 0.00m;
        public decimal TotalOrderAmount { get; set; } = 0.00m;
        public string OrderNo { get; set; } = "None";
        public string PhoneNumber { get; set; } = "None";
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;
        public string CardNo { get; set; } = "None";
        public string CVV { get; set; } = "None";
        public decimal BalanceRemaining { get; set; } = 0.00m;
        public string Status { get; set; } = "Pending";
        public string UpdatedBy { get; set; } = "None";
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
        public string PaymentMethod { get; set; } = "None";
        public decimal TotalAmountPaid { get; set; } = 0.00m;

    }
}
