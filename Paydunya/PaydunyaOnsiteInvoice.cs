using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Paydunya
{
    public class PaydunyaOnsiteInvoice : PaydunyaCheckoutInvoice
    {
        public string InvoiceToken;
        public PaydunyaOnsiteInvoice(PaydunyaSetup setup, PaydunyaStore store)
            : base(setup, store)
        {

        }

        public bool Create(string AccountAlias)
        {
            invoice.Add("items", items);
            invoice.Add("taxes", taxes); 

            JObject payload = new JObject
            {
                { 
                    "invoice_data", new JObject
                        {
                            { "invoice", invoice },
                            { "store", storeData },
                            { "actions", actions },
                            { "custom_data", customData }
                        }
                },
                { 
                   "opr_data", new JObject
                        {
                            { "account_alias", AccountAlias }
                        }
                }
            };

            string jsonData = JsonConvert.SerializeObject(payload);

            JObject JsonResult = utility.HttpPostJson(PayDunyaHelper.GetOPRInvoiceUrl(setup.Mode), jsonData);
            ResponseCode = JsonResult["response_code"].ToString();
            if (ResponseCode == "00")
            {
                Status = PaydunyaCheckout.SUCCESS;
                ResponseText = JsonResult["description"].ToString();
                Token = JsonResult["token"].ToString();
                InvoiceToken = JsonResult["invoice_token"].ToString();
                return true;
            }
            else
            {
                ResponseText = JsonResult["response_text"].ToString();
                Status = PaydunyaCheckout.FAIL;
            }
            return false ;
        }

        public bool Charge(string OPRToken, string ConfirmToken)
        {

            string jsonData = JsonConvert.SerializeObject(
                new JObject
                {
                    { "token", OPRToken },
                    { "confirm_token", ConfirmToken }
                });

            JObject JsonResult = utility.HttpPostJson(PayDunyaHelper.GetOPRChargeUrl(setup.Mode), jsonData);

            if (JsonResult.Count > 0)
            {
                if (JsonResult["response_code"].ToString() == "00")
                {
                    Status = JsonResult["invoice_data"]["status"].ToString();

                    invoice = utility.ParseJSON(JsonResult["invoice_data"]["invoice"]);
                    taxes = utility.ParseJSON(JsonResult["invoice_data"]["taxes"]);
                    customData = utility.ParseJSON(JsonResult["invoice_data"]["custom_data"]);
                    storeData = utility.ParseJSON(JsonResult["invoice_data"]["store"]);
                    customer = utility.ParseJSON(JsonResult["invoice_data"]["customer"]);
                    receiptUrl = JsonResult["invoice_data"]["receipt_url"].ToString();

                    ResponseText = JsonResult["response_text"].ToString();
                    ResponseCode = "00";
                    return true;
                }
                else
                {
                    Status = PaydunyaCheckout.FAIL;
                    ResponseText = JsonResult["response_text"].ToString();
                    ResponseCode = JsonResult["response_code"].ToString();
                }
            }
            else
            {
                Status = PaydunyaCheckout.FAIL;
                ResponseCode = "1002";
                ResponseText = "Invoice Not Found";
            }

            return false;
        }
    }
}

