namespace EccomerceWebsiteProject.Core.Models.STK_responses
{
    public class CallbackRequest
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
        public string OrderNo { get; set; } // Add this field to capture the order number
        public decimal Amount { get; set; }
        public string PhoneNumber { get; set; }
    }
    


    }
