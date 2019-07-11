using System;

namespace Paydunya
{
    public class PaydunyaCheckout
    {
        public string Status { get; set; }
        public string ResponseText { get; set; }
        public string ResponseCode { get; set; }
        public string Token { get; set; }

        public const string FAIL = "fail";
        public const string SUCCESS = "success";


        public PaydunyaCheckout()
        {
        }
    }
}

