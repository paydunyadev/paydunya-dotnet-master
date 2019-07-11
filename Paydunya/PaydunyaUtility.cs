using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace Paydunya
{
    public class PaydunyaUtility
    {
        private PaydunyaSetup setup;

        public PaydunyaUtility(PaydunyaSetup setup)
        {
            this.setup = setup;
        }

        public JObject HttpPostJson(string url, string payload)
        {
            var bytes = Encoding.Default.GetBytes(payload);

            WebClient client = new WebClient();
            client.Headers.Add(HttpRequestHeader.UserAgent,
                   "Paydunya Checkout API .NET client v1 aka Neptune");

            client.Headers.Add("PAYDUNYA-MASTER-KEY", setup.MasterKey);
            client.Headers.Add("PAYDUNYA-PRIVATE-KEY", setup.PrivateKey);
            client.Headers.Add("PAYDUNYA-PUBLIC-KEY", setup.PublicKey);
            client.Headers.Add("PAYDUNYA-TOKEN", setup.Token);
            client.Headers.Add("PAYDUNYA-MODE", setup.Mode);
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

            var response = client.UploadData(url, "POST", bytes);

            return JObject.Parse(Encoding.Default.GetString(response));
        }

        public JObject HttpGetRequest(string url)
        {
            WebClient client = new WebClient();
            client.Headers.Add(HttpRequestHeader.UserAgent,
                   "Paydunya Checkout API .NET client v1 aka Neptune");

            client.Headers.Add("PAYDUNYA-MASTER-KEY", setup.MasterKey);
            client.Headers.Add("PAYDUNYA-PRIVATE-KEY", setup.PrivateKey);
            client.Headers.Add("PAYDUNYA-PUBLIC-KEY", setup.PublicKey);
            client.Headers.Add("PAYDUNYA-TOKEN", setup.Token);
            client.Headers.Add("PAYDUNYA-MODE", setup.Mode);

            var response = client.DownloadString(url);

            return JObject.Parse(response);
        }

        public JObject ParseJSON(object jsontext)
        {
            string JsonString = "{}";

            try
            {
                JsonString = jsontext.ToString();
            }
            catch (NullReferenceException)
            {
            }

            return JObject.Parse(JsonString);
        }

    }
}

