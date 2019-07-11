using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Paydunya
{
    public class PaydunyaDirectPay
    {
        public string Status { get; set; }
        public string ResponseText { get; set; }
        public string ResponseCode { get; set; }
        public string Description { get; set; }
        public string TransactionId { get; set; }

        public const string FAIL = "fail";
        public const string SUCCESS = "success";

        protected PaydunyaUtility utility;

        private PaydunyaSetup setup;

        public PaydunyaDirectPay(PaydunyaSetup setup)
        {
            this.setup = setup;
            this.utility = new PaydunyaUtility(setup);
        }

        public bool CreditAccount(string PaydunyaAccount, double Amount)
        {
            bool result = false;
            JObject payload = new JObject();

            payload.Add("account_alias", PaydunyaAccount);
            payload.Add("amount", Amount);
            string jsonData = JsonConvert.SerializeObject(payload);

            JObject JsonResult = utility.HttpPostJson(setup.GetDirectPayCreditUrl(), jsonData);

            ResponseCode = JsonResult["response_code"].ToString();
            if (ResponseCode == "00")
            {
                Status = SUCCESS;
                ResponseText = JsonResult["response_text"].ToString();
                Description = JsonResult["description"].ToString();
                TransactionId = JsonResult["transaction_id"].ToString();
                result = true;
            }
            else
            {
                ResponseText = JsonResult["response_text"].ToString();
                Status = FAIL;
            }
            return result;
        }
    }
}

