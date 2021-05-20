using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paydunya
{
    public static class PayDunyaHelper
    {
        public static JObject SetPaydunyaStore(PaydunyaStore store)
        {
            return new JObject
            {
                { "name", store.Name },
                { "tagline", store.Tagline },
                { "postal_address", store.PostalAddress },
                { "website_url", store.WebsiteUrl },
                { "phone_number", store.PhoneNumber },
                { "logo_url", store.LogoUrl }
            };
        }
    }
}
