using System.ComponentModel.DataAnnotations;

namespace EccomerceWebsiteProject.Core.Models.STK_responses
{
    public class STK_Responses
    {
        [Key]
        public Guid TransactionID { get; set; }
        public string MerchantRequestID { get; set; }
        public string CheckoutRequestID { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string CustomerMessage { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
