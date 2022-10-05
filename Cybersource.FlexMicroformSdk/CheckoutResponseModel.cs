using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cybersource.FlexMicroformSdk
{    
    public class CheckoutResponseModel
    {
        public string utf8 { get; set; }
        public string auth_cv_result { get; set; }
        public string req_locale { get; set; }
        public string score_score_result { get; set; }
        public string decision_active_profile_selector_rule { get; set; }
        public string req_bill_to_surname { get; set; }
        public string score_rflag { get; set; }
        public string score_card_issuer { get; set; }
        public string req_ignore_avs { get; set; }
        public string score_rcode { get; set; }
        public string req_bill_to_phone { get; set; }
        public string req_ignore_cvn { get; set; }
        public string auth_amount { get; set; }
        public string auth_response { get; set; }
        public string bill_trans_ref_no { get; set; }
        public string auth_time { get; set; }
        public string decision_early_return_code { get; set; }
        public string transaction_id { get; set; }
        public string score_ip_city { get; set; }
        public string req_override_custom_receipt_page { get; set; }
        public string score_velocity_info { get; set; }
        public string auth_avs_code { get; set; }
        public string auth_code { get; set; }
        public string score_address_info { get; set; }
        public string payment_token_instrument_identifier_new { get; set; }
        public string score_factors { get; set; }
        public string score_phone_info { get; set; }
        public string score_model_used { get; set; }
        public string req_bill_to_address_country { get; set; }
        public string req_transient_token { get; set; }
        public string auth_cv_result_raw { get; set; }
        public string decision_rmsg { get; set; }
        public string req_profile_id { get; set; }
        public string score_suspicious_info { get; set; }
        public string decision_rcode { get; set; }
        public string score_rmsg { get; set; }
        public string req_partner_solution_id { get; set; }
        public string decision_rflag { get; set; }
        public DateTime signed_date_time { get; set; }
        public string req_bill_to_address_line1 { get; set; }
        public string score_ip_state { get; set; }
        public string score_ip_country { get; set; }
        public string signature { get; set; }
        public string score_card_scheme { get; set; }
        public string score_ip_routing_method { get; set; }
        public string score_bin_country { get; set; }
        public string payment_token { get; set; }
        public string payment_token_instrument_identifier_id { get; set; }
        public string req_bill_to_address_city { get; set; }
        public string req_bill_to_address_postal_code { get; set; }
        public string score_reason_code { get; set; }
        public string reason_code { get; set; }
        public string req_bill_to_forename { get; set; }
        public string request_token { get; set; }
        public string score_card_account_type { get; set; }
        public string req_amount { get; set; }
        public string req_bill_to_email { get; set; }
        public string auth_avs_code_raw { get; set; }
        public string req_currency { get; set; }
        public string decision { get; set; }
        public string decision_return_code { get; set; }
        public string message { get; set; }
        public string signed_field_names { get; set; }
        public string req_transaction_uuid { get; set; }
        public string decision_reason_code { get; set; }
        public string score_time_local { get; set; }
        public string score_return_code { get; set; }
        public string score_host_severity { get; set; }
        public string req_transaction_type { get; set; }
        public string req_override_backoffice_post_url { get; set; }
        public string req_access_key { get; set; }
        public string score_internet_info { get; set; }
        public string decision_early_reason_code { get; set; }
        public string req_reference_number { get; set; }
        public string decision_active_profile { get; set; }
        public string req_bill_to_address_state { get; set; }
        public string payment_token_instrument_identifier_status { get; set; }
        public string decision_early_rcode { get; set; }
    }
}