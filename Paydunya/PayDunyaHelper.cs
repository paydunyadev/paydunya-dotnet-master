using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paydunya
{
    public static class PayDunyaHelper
    {

        public const string ROOT_URL_BASE = "https://app.paydunya.com";
        public const string LIVE_CHECKOUT_INVOICE_BASE_URL = "/api/v1/checkout-invoice/create";
        public const string TEST_CHECKOUT_INVOICE_BASE_URL = "/sandbox-api/v1/checkout-invoice/create";
        public const string LIVE_CHECKOUT_CONFIRM_BASE_URL = "/api/v1/checkout-invoice/confirm/";
        public const string TEST_CHECKOUT_CONFIRM_BASE_URL = "/sandbox-api/v1/checkout-invoice/confirm/";
        public const string LIVE_OPR_BASE_URL = "/api/v1/opr/create";
        public const string TEST_OPR_BASE_URL = "/sandbox-api/v1/opr/create";
        public const string LIVE_OPR_CHARGE_BASE_URL = "/api/v1/opr/charge";
        public const string TEST_OPR_CHARGE_BASE_URL = "/sandbox-api/v1/opr/charge";
        public const string LIVE_DIRECT_PAY_CREDIT_URL = "/api/v1/direct-pay/credit-account";
        public const string TEST_DIRECT_PAY_CREDIT_URL = "/sandbox-api/v1/direct-pay/credit-account";



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

        public static string GetConfirmUrl(string Mode)
        {
            return Mode == "live" ? PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.LIVE_CHECKOUT_CONFIRM_BASE_URL
                                       : PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.TEST_CHECKOUT_CONFIRM_BASE_URL;
        }

        public static string GetInvoiceUrl(string Mode)
        {
            return Mode == "live" ? PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.LIVE_CHECKOUT_INVOICE_BASE_URL
                                       : PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.TEST_CHECKOUT_INVOICE_BASE_URL;
        }

        public static string GetOPRInvoiceUrl(string Mode)
        {
            return Mode == "live" ? PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.LIVE_OPR_BASE_URL
                                       : PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.TEST_OPR_BASE_URL;
        }

        public static string GetOPRChargeUrl(string Mode)
        {
            return Mode == "live" ? PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.LIVE_OPR_CHARGE_BASE_URL
                                       : PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.TEST_OPR_CHARGE_BASE_URL;
        }

        public static string GetDirectPayCreditUrl(string Mode)
        {
            return Mode == "live" ? PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.LIVE_DIRECT_PAY_CREDIT_URL
                                       : PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.TEST_DIRECT_PAY_CREDIT_URL;
        }
    }
}
