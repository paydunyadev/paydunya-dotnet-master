using System;

namespace Paydunya
{
    public class PaydunyaStore
    {
        public string Name { get; set; }
        public string Tagline { get; set; }
        public string PostalAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteUrl { get; set; }
        public string CancelUrl { get; set; }
        public string ReturnUrl { get; set; }
		public string CallbackUrl { get; set; }
        public string LogoUrl { get; set; }


        public PaydunyaStore()
        {
        }
    }
}

