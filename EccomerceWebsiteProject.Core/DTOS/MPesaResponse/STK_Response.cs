namespace EccomerceWebsiteProject.Core.DTOS.MPesaResponse
{
    public class STK_Response
    {
        public string MerchantRequestID { get; set; }
        public string CheckoutRequestID { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string CustomerMessage { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
