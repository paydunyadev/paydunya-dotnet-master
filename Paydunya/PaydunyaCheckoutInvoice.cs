using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Paydunya
{
    public class PaydunyaCheckoutInvoice : PaydunyaCheckout
    {
        protected PaydunyaSetup setup;
        protected PaydunyaStore store;
        protected JObject invoice = new JObject();
        protected JObject storeData = new JObject();
        protected JObject items = new JObject();
        protected JObject taxes = new JObject();
        protected JArray channels = new JArray();
        protected JObject customData = new JObject();
        protected JObject customer = new JObject();
        protected JObject actions = new JObject();
        protected PaydunyaUtility utility;
        protected string invoiceUrl { get; set; }
        protected string cancelUrl { get; set; }
        protected string returnUrl { get; set; }
		protected string callbackUrl { get; set; }
		protected string receiptUrl { get; set; }

        public PaydunyaCheckoutInvoice(PaydunyaSetup setup, PaydunyaStore store)
        {
            this.setup = setup;
            this.store = store;
            this.utility = new PaydunyaUtility(setup);

            storeData.Add("name", this.store.Name);
            storeData.Add("tagline", this.store.Tagline);
            storeData.Add("postal_address", this.store.PostalAddress);
            storeData.Add("website_url", this.store.WebsiteUrl);
            storeData.Add("phone_number", this.store.PhoneNumber);
            storeData.Add("logo_url", this.store.LogoUrl);

            if(!string.IsNullOrEmpty(this.store.CancelUrl))
            {
               actions.Add("cancel_url", this.store.CancelUrl);
            }

            if (!string.IsNullOrEmpty(this.store.ReturnUrl))
            {
               actions.Add("return_url", this.store.ReturnUrl);
            }

			if (!string.IsNullOrEmpty(this.store.ReturnUrl))
			{
				actions.Add("callback_url", this.store.CallbackUrl);
			}
      
        }

        public void AddItem(string name, int quantity, double price, double total_price, string description = "")
        {
            JObject item = new JObject();
            item.Add("name", name);
            item.Add("quantity", quantity);
            item.Add("unit_price", price);
            item.Add("total_price", total_price);
            item.Add("description", description);
            items.Add("items_" + items.Count.ToString(), item);
        }

        public void AddTax(string name, double amount)
        {
            JObject tax = new JObject();
            tax.Add("name", name);
            tax.Add("amount", amount);
            taxes.Add("taxes_" + (string)taxes.Count.ToString(), tax);
        }

		public void AddChannel(string channel)
		{
			channels.Add(channel);
		}

        public void AddChannels(string[] channels)
        {
            this.channels = new JArray();

            foreach (string channel in channels)
            {
                this.channels.Add(channel);
            }
        }

        public void SetTotalAmount(double amount)
        {
            invoice.Add("total_amount", amount);
        }

        public void SetDescription(string description)
        {
            invoice.Add("description", description);
        }

        public void SetCancelUrl(string url)
        {
            actions.Add("cancel_url", url);
        }

        public void SetReturnUrl(string url)
        {
            actions.Add("return_url", url);
        }

		public void SetCallbackUrl(string url)
		{
			actions.Add("callback_url", url);
		}

        public void SetInvoiceUrl(string url)
        {
            invoiceUrl = url;
        }

        public string GetInvoiceUrl()
        {
            return invoiceUrl;
        }

        public string GetReceiptUrl()
        {
            return receiptUrl;
        }

        public string GetCancelUrl()
        {
            return (string)actions["cancel_url"];
        }

        public string GetReturnUrl()
        {
            return (string)actions["return_url"];
        }

		public string GetCallbackUrl()
		{
			return (string)actions["callback_url"];
		}

        public double GetTotalAmount()
        {
            return (double)invoice["total_amount"];
        }

        public void SetCustomData(string key, object value)
        {
            customData.Add(key, JToken.FromObject(value));
        }

        public object GetCustomData(string key)
        {
            return customData[key];
        }

        public object GetCustomerInfo(string key)
        {
            return customer[key];
        }

        public bool Create()
        {
            bool result = false;
            JObject payload = new JObject();
            invoice.Add("items", items);
            invoice.Add("taxes", taxes);
            invoice.Add("channels", channels);
            payload.Add("invoice", invoice);
            payload.Add("store", storeData);
            payload.Add("actions", actions);
            payload.Add("custom_data", customData);
            string jsonData = JsonConvert.SerializeObject(payload);
            JObject jsonResult = utility.HttpPostJson(setup.GetInvoiceUrl(), jsonData);
            ResponseCode = jsonResult["response_code"].ToString();
            if (ResponseCode == "00")
            {
                Status = PaydunyaCheckout.SUCCESS;
                SetInvoiceUrl(jsonResult["response_text"].ToString());
                ResponseText = jsonResult["description"].ToString();
                Token = jsonResult["token"].ToString();
                result = true;
            }
            else
            {
                ResponseText = jsonResult["response_text"].ToString();
                Status = PaydunyaCheckout.FAIL;
            }
            return result;
        }

        public bool Confirm(string token)
        {
            JObject jsonData = utility.HttpGetRequest(setup.GetConfirmUrl() + token);
            bool result = false;

            if (jsonData.Count > 0 && jsonData["status"] != null)
            {
                Status = jsonData["status"].ToString();
                invoice = utility.ParseJSON(jsonData["invoice"]);
                taxes = utility.ParseJSON(jsonData["taxes"]);
                customData = utility.ParseJSON(jsonData["custom_data"]);
                storeData = utility.ParseJSON(jsonData["store"]);

                if (jsonData["status"].ToString() == "completed")
                {
                    ResponseText = "Checkout Invoice has been paid";
                    ResponseCode = "00";
                    result = true;
                    customer = utility.ParseJSON(jsonData["customer"]);
                    receiptUrl = (string)jsonData["receipt_url"];
                }
                else
                {
                    ResponseText = "Checkout Invoice has not been paid";
                    ResponseCode = "1003";
                }
            }
            else
            {
                Status = PaydunyaCheckout.FAIL;
                ResponseCode = "1002";
                ResponseText = "Invoice Not Found";
            }

            return result;
        }
    }
}

