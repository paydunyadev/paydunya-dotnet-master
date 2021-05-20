using System;

namespace Paydunya
{
    public class PaydunyaSetup
    {
        public string MasterKey { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Token { get; set; }
        public string Mode { get; set; }

        public PaydunyaSetup()
        {
        }

        public PaydunyaSetup(string MasterKey, string PublicKey, string PrivateKey, string Token, string Mode)
        {
            this.MasterKey = MasterKey;
            this.PublicKey = PublicKey;
            this.PrivateKey = PrivateKey;
            this.Token = Token;
            this.Mode = Mode;
        }

        public string GetConfirmUrl()
        {
            return this.Mode == "live" ? PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.LIVE_CHECKOUT_CONFIRM_BASE_URL 
                                       : PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.TEST_CHECKOUT_CONFIRM_BASE_URL;
        }

        public string GetInvoiceUrl()
        {
            return this.Mode == "live" ? PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.LIVE_CHECKOUT_INVOICE_BASE_URL 
                                       : PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.TEST_CHECKOUT_INVOICE_BASE_URL;
        }

        public string GetOPRInvoiceUrl()
        {
            return this.Mode == "live" ? PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.LIVE_OPR_BASE_URL 
                                       : PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.TEST_OPR_BASE_URL;
        }

        public string GetOPRChargeUrl()
        {
            return this.Mode == "live" ? PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.LIVE_OPR_CHARGE_BASE_URL 
                                       : PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.TEST_OPR_CHARGE_BASE_URL;
        }

        public string GetDirectPayCreditUrl()
        {
            return this.Mode == "live" ? PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.LIVE_DIRECT_PAY_CREDIT_URL 
                                       : PayDunyaHelper.ROOT_URL_BASE + PayDunyaHelper.TEST_DIRECT_PAY_CREDIT_URL;
        }
    }
}

