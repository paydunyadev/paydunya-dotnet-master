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
    }
}

