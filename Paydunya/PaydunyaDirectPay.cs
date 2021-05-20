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

        private readonly PaydunyaSetup setup;

        public PaydunyaDirectPay(PaydunyaSetup setup)
        {
            this.setup = setup;
            this.utility = new PaydunyaUtility(setup);
        }

        public bool CreditAccount(string PaydunyaAccount, double Amount)
        {
            string jsonData = JsonConvert.SerializeObject(
                new JObject
                {
                    { "account_alias", PaydunyaAccount },
                    { "amount", Amount }
                });

            JObject JsonResult = utility.HttpPostJson(PayDunyaHelper.GetDirectPayCreditUrl(setup.Mode), jsonData);

            ResponseCode = JsonResult["response_code"].ToString();
            if (ResponseCode == "00")
            {
                Status = SUCCESS;
                ResponseText = JsonResult["response_text"].ToString();
                Description = JsonResult["description"].ToString();
                TransactionId = JsonResult["transaction_id"].ToString();
                return true;
            }
            else
            {
                ResponseText = JsonResult["response_text"].ToString();
                Status = FAIL;
            }
            return false;
        }
    }
}

