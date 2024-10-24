using System.Text.Json.Serialization;

namespace EccomerceWebsiteProject.Core.DTOS.MPesaResponse
{
    public class CallBackResponse
    {
            public Body Body { get; set; }
        }

        public class Body
        {
            public StkCallback StkCallback { get; set; }
        }

        public class StkCallback
        {
            public string MerchantRequestID { get; set; }
            public string CheckoutRequestID { get; set; }
            public int ResultCode { get; set; }
            public string ResultDesc { get; set; }
        }


    }
