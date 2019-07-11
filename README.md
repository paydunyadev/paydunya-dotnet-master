PAYDUNYA .NET Client API
======================
PAYDUNYA .NET Client Library

## Offical Documentation
https://paydunya.com/developers/dotnet

## Installation

Add Assembly file Dependencies from the `bin/Release` directory

    Paydunya.dll
    Newtonsoft.Json.dll

## Setup your API Keys

    PaydunyaSetup setup = new PaydunyaSetup {
      MasterKey = "wQzk9ZwR-Qq9m-0hD0-zpud-je5coGC3FHKW",
      PrivateKey = "test_private_rMIdJM3PLLhLjyArx9tF3VURAF5",
      PublicKey = "test_public_kb9Wo0Qpn8vNWMvMZOwwpvuTUja",
      Token = "IivOiOxGJuWhc5znlIiK",
      Mode = "test"
    };

## Setup your checkout store information

    PaydunyaStore store = new PaydunyaStore {
      Name = "Magasin Chez Sandra",
      Tagline = "L'elegance n'a pas de prix",
      PhoneNumber = "336530583",
      PostalAddress = "Dakar Plateau - Etablissement kheweul"
    };

Customer will be redirected back to this URL when he cancels the checkout on Paydunya website

    store.CancelUrl = "CHECKOUT_CANCEL_URL";

Paydunya will automatically redirect customer to this URL after successfull payment.
This setup is optional and highly recommended you dont set it, unless you want to customize the payment experience of your customers.
When a returnURL is not set, Paydunya will redirect the customer to the receipt page.

    store.ReturnUrl = "CHECKOUT_RETURN_URL";

## Create your Checkout Invoice
Please note that `PaydunyaCheckoutInvoice` Class requires two parameters which should be instances of PaydunyaSetup & PaydunyaStore respectively

    PaydunyaCheckoutInvoice co = new PaydunyaCheckoutInvoice (setup, store);

## Create your Onsite Payment Request Invoice
Please note that `PaydunyaOnsiteInvoice` Class requires two parameters which should be instances of PaydunyaSetup & PaydunyaStore respectively

    PaydunyaOnsiteInvoice co = new PaydunyaOnsiteInvoice (setup, store);

Params for addItem function `AddItem(name_of_item,quantity,unit_price,total_price,[description])`

      co.AddItem("Clavier DELL", 2, 3000, 6000);
      co.AddItem("Ordinateur Lenovo L440", 1, 400000, 400000, "Description optionelle");
      co.AddItem("Casque Logitech", 1, 8000, 8000);

## Set the total amount to be charged ! Important

    co.SetTotalAmount(414000);

## Setup Tax Information (Optional)

    co.AddTax("TVA (18%)", 5000);
    co.AddTax("Autre taxe", 700);

## You can add custom data to your invoice which can be called back later

    co.SetCustomData("Prenom", "Badara");
    co.SetCustomData("Nom", "Alioune");
    co.SetCustomData("CartId", 97628);
    co.SetCustomData("Coupon","NOEL");

## Pushing invoice to PAYDUNYA server and getting your URL

    if(co.Create()) {
      Console.WriteLine (co.ResponseText);
      Console.WriteLine ("Invoice URL: "+co.GetInvoiceUrl());
    }else{
      Console.WriteLine ("Error Message: "+co.ResponseText);
    }

## Onsite Payment Request(PSR) Charge
First step is to take the customers PAYDUNYA account alias, this could be the phoneno, username or PAYDUNYA account number.
pass this as a param for the `create` action of the `Paydunya::Onsite::Invoice` class instance. PAYDUNYA will return an PSR TOKEN after the request is successfull. The customer will also receieve a confirmation TOKEN.

    if(co.Create("CUSTOMER_PAYDUNYA_EMAIL_OR_PHONE")) {
      Console.WriteLine (co.ResponseText);
      Console.WriteLine ("PSR Token: "+co.Token);
    }else{
      Console.WriteLine ("Error Message: "+co.ResponseText);
    }

Second step requires you to accept the confirmation TOKEN from the customer, add your PSR Token and issue the charge. Upon successfull charge you should be able to access the digital receipt URL and other objects outlined in the offical docs.

    if(co.Charge("PSR_TOKEN","CUSTOMER_CONFIRM_TOKEN")) {
      Console.WriteLine (co.ResponseText);
      Console.WriteLine ("Receipt URL: "+co.GetReceiptUrl());
    }else{
      Console.WriteLine ("Error Message: "+co.ResponseText);
    }

## TFA Request
You can pay any PAYDUNYA account directly via your third party apps. This is particularly excellent for implementing your own Adaptive payment solutions ontop of PAYDUNYA. Please note that `PaydunyaDirectPay` Class expects one parameter which should be an instance of the PaydunyaSetup Class

    PaydunyaDirectPay direct_pay = new PaydunyaDirectPay (setup);
    if(direct_pay.CreditAccount("Paydunya_CUSTOMER_USERNAME_OR_PHONENO",50)){
      Console.WriteLine(direct_pay.Description);
      Console.WriteLine (direct_pay.Status);
      Console.WriteLine (direct_pay.ResponseText);
    }else{
      Console.WriteLine(direct_pay.ResponseText);
      Console.WriteLine (direct_pay.Status);
    }


## Download PAYDUNYA .NET Demo
https://github.com/paydunya/paydunya-dotnet-demo

## Contribuer

1. Faire un Fork de ce dépôt
2. Créer une nouvelle branche (`git checkout -b new-feature`)
3. Faire un commit de vos modifications (`git commit -am "Ajout d'une nouvelle fonctionnalité"`)
4. Faire un Push au niveau de la branche (`git push origin new-feature`)
5. Créer un Pull Request