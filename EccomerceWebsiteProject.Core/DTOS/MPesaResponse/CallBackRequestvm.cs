namespace EccomerceWebsiteProject.Core.DTOS.MPesaResponse
{
    public class CallBackRequestvm
    {
        public string MerchantRequestID { get; set; }
        public string CheckoutRequestID { get; set; }
        public int ResultCode { get; set; }
        public string ResultDesc { get; set; }
    }
}
