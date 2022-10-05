using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cybersource.FlexMicroformSdk
{
    
    public class CheckoutApiInitialization
    {
        public string profile_id { get; set; }
        public string access_key { get; set; }
        public string reference_number { get; set; }
        public string transaction_uuid { get; set; }
        public string transaction_type { get; set; }

        [JsonProperty("payment_method:")]
        public string PaymentMethod { get; set; }
        public string bill_to_forename { get; set; }
        public string bill_to_surname { get; set; }
        public string bill_to_phone { get; set; }
        public string bill_to_email { get; set; }
        public string bill_to_address_line1 { get; set; }
        public string bill_to_address_city { get; set; }
        public string bill_to_address_state { get; set; }
        public string bill_to_address_postal_code { get; set; }
        public string bill_to_address_country { get; set; }
        public string payer_authentication_transaction_mode { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
        public string locale { get; set; }
        public string override_backoffice_post_url { get; set; }
        public string override_custom_receipt_page { get; set; }
        public string ignore_avs { get; set; }
        public string ignore_cvn { get; set; }
        public string partner_solution_id { get; set; }
        public string payer_authentication_challenge_code { get; set; }
        public string unsigned_field_names { get; set; }        
    }

    
    public class CaptureContextRequestModel
    {
        public List<string> targetOrigins { get; set; }
        public CheckoutApiInitialization checkoutApiInitialization { get; set; }

        public CaptureContextRequestModel()
        {
            this.targetOrigins = new List<string>();
            this.checkoutApiInitialization = new CheckoutApiInitialization();
        }
    }
}