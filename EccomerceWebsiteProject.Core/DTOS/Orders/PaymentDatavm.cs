using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EccomerceWebsiteProject.Core.DTOS.Orders
{
    public class PaymentDatavm
    {
        public decimal AmountPaid { get; set; }
        public decimal TotalOrderAmount { get; set; }
        public string OrderNo { get; set; }
        public string PhoneNumber { get; set; } = "None";
        public DateTime ExpiryDate { get; set; }= DateTime.UtcNow;
        public string CardNo { get; set; } = "None";
        public string CVV { get; set; } = "None";
        public string PaymentMethod { get; set; }
    }
}
