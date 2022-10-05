using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Cybersource.FlexMicroformSdk;
using System.Text;

namespace BookingDotcom
{
    public partial class cybersource_payment : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                //should pass value as string true do not move it inside postback call
                bool isTest;
                Boolean.TryParse(Request.QueryString["Test"], out isTest);

                IDictionary<string, string> transResponse = new Dictionary<string, string>();
                foreach (var key in Request.Form.AllKeys)
                {
                    transResponse.Add(key, Request.Params[key]);
                }

                //responce came from cybersource else client call
                if (transResponse.ContainsKey("signature") && transResponse.ContainsKey("req_profile_id")) 
                {
                    try
                    {
                        //checking if responce came from cybersource or imposter
                        if (transResponse["signature"].Equals(Security.sign(transResponse, GenerateSecurityKey(isTest, transResponse["req_profile_id"]))))
                        {
                            //It is advice to store response message in DB if processing any server side error occures responce can be retrived.
                            string responseString = Utility.MyDictionaryToJson(transResponse);

                            CheckoutResponseModel checkoutResponse = JsonConvert.DeserializeObject<CheckoutResponseModel>(responseString);
                            PaymentProcessed(checkoutResponse);

                            //throw new Exception("Testing catch block");
                            //doing this step only to pass partial value so we do not expose complete response in parent javascript
                            //Modify this message to pass any value from iframe to parent page
                            //Do not pass sesative data like profile_id etc as that in vernable to javascript injections
                            string messageResponce = ProcessMessageResponce(checkoutResponse);
                            string javascriptFunctionCall = string.Format("parent.callfromIFrame({0})", messageResponce);
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "parentPageCall", javascriptFunctionCall, true);
                        }
                    }
                    catch
                    {
                        //error to display in parent page as coming during processing payment hence javascript call process serverside as well 
                        //before calling client side
                        string javascriptFunctionCall = string.Format("parent.callfromIFrameError(\"{0}\")", "These is some server side error during process, please contact customer care if amount is deducted");
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "parentPageCallError", javascriptFunctionCall, true);
                    }
                }
                else
                {
                    try
                    {
                        ViewState["CaptureContext"] = JsonConvert.SerializeObject(GenerateCaptureContext(isTest));
                        ViewState["CallApiConfig"] = JsonConvert.SerializeObject(GenerateAPIConfigPayload(isTest));
                        ViewState["checkoutPaymentUrl"] = GenerateCheckoutAPIURL(isTest);
                    }
                    catch (Exception exec)
                    {
                        //error during page load and configration
                        errorsDetails.Style.Add("display", "block");
                        errorsDetails.InnerHtml = string.Format("Server side error {0}", exec.Message);
                    }
                }
            }
            
            //SubscribeAutoPay(); <== implemented for demo perpose should be done from diffrent class
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkoutResponse"></param>
        /// <returns></returns>
        private string ProcessMessageResponce(CheckoutResponseModel checkoutResponse)
        {
            StringBuilder preparMessage = new StringBuilder();

            preparMessage.Append("{\n");
            preparMessage.AppendFormat(" \"decision\": \"{0}\",\n", checkoutResponse.decision);
            preparMessage.AppendFormat(" \"decision_reason_code\": \"{0}\",\n", string.IsNullOrEmpty(checkoutResponse.decision_reason_code) ? "" : checkoutResponse.decision_reason_code);
            preparMessage.AppendFormat(" \"message\": \"{0}\",\n", checkoutResponse.message);
            preparMessage.AppendFormat(" \"transaction_uuid\": \"{0}\"\n", checkoutResponse);
            preparMessage.Append("}");

            return preparMessage.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Continue_Click1(object sender, EventArgs e)
        {
            try
            {                
                CallAPIConfigModel callApiConfig = JsonConvert.DeserializeObject<CallAPIConfigModel>(Convert.ToString(ViewState["CallApiConfig"]));
                string captureContext = CallCybersourceAPI.CallCaptureContext(UpdateCaptureContextPayload(), callApiConfig);
                if (CallCybersourceAPI.ValidateCaptureContext(captureContext, callApiConfig))
                {
                    string javascriptFunctionCall = string.Format("GenerateMicroForm(\"{0}\")", captureContext);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallGenerateMicroForm", javascriptFunctionCall, true);
                }
            }
            catch (Exception exec)
            {
                errorsDetails.Style.Add("display", "block");
                errorsDetails.InnerHtml = string.Format("Server side error {0}", exec.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CaptureContextRequestModel UpdateCaptureContextPayload()
        {
            CaptureContextRequestModel captureContextPayload = JsonConvert.DeserializeObject<CaptureContextRequestModel>(Convert.ToString(ViewState["CaptureContext"]));
            captureContextPayload.checkoutApiInitialization.bill_to_forename = Forename.Text;
            captureContextPayload.checkoutApiInitialization.bill_to_surname = Surname.Text;
            captureContextPayload.checkoutApiInitialization.bill_to_email = EmailAddress.Text;
            captureContextPayload.checkoutApiInitialization.bill_to_phone = PhoneNumber.Text;
            captureContextPayload.checkoutApiInitialization.bill_to_address_line1 = AddressLine1.Text;
            captureContextPayload.checkoutApiInitialization.bill_to_address_city = City.Text;
            captureContextPayload.checkoutApiInitialization.bill_to_address_state = State.Text;
            captureContextPayload.checkoutApiInitialization.bill_to_address_country = Country.Value;
            captureContextPayload.checkoutApiInitialization.bill_to_address_postal_code = Postcode.Text;
            bool isFirstTimeSubscriber = SaveCardDetails.Checked; //true if subscription is needed for auto pay,we want to create token for CIT & MITUnschuled
            captureContextPayload.checkoutApiInitialization.transaction_type = isFirstTimeSubscriber ? "sale,create_payment_token" : "sale";
            return captureContextPayload;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        private void PaymentProcessed(CheckoutResponseModel response)
        {

            if (response.decision == "ACCEPT" && Convert.ToInt16(response.decision_reason_code) == 100)
            {

                double amount = Convert.ToDouble(response.req_amount) / 100.0;
                string orderCode = response.req_reference_number;

                if (String.IsNullOrEmpty(response.payment_token))
                {
                    //if payment token is available that means in MITUnscheadule for AutoSubscriptionPay
                    //please see code #region Autopay - to be implemented in different class
                }

                //  SendOperatorPaymentEmail(request, amount, orderCode, booking);

                //if (bSite.ThisSite.PaymentProviderSettings.SendCustomerWorldpayReceipts)
                //   SendCustomerPaymentEmail(request, amount, orderCode);
            }
            else
            {
                ProcessCybersourceFailure(response);
            }
        }

        /// <summary>
        /// Process falure to database
        /// </summary>
        /// <param name="response"></param>
        private void ProcessCybersourceFailure(CheckoutResponseModel response)
        {
            var reasonCode = Utility.ReadEmbededLanguageFile();

            string errorMessage = string.Format("error message: {0}, reason for error: Unknown", response.message); ;

            if (!string.IsNullOrEmpty(response.decision_reason_code) && reasonCode.ContainsKey(response.decision_reason_code))
            {
                errorMessage = string.Format("error message: {0}, reason for error: {1}", response.message, reasonCode[response.decision_reason_code]);
            }

            //Process errorMessage
        }

        /// <summary>
        /// This will be used in aspx Form submit
        /// can change in future should come from config file or db
        /// </summary>
        /// <param name="isTest"></param>
        /// <exception cref="NotImplementedException"></exception>
        private string GenerateCheckoutAPIURL(bool isTest)
        {
            string checkoutPaymentUrl = string.Empty;
            if (isTest)
            {
                checkoutPaymentUrl = "https://testsecureacceptance.cybersource.com/silent/pay";
            }
            else
            {
                checkoutPaymentUrl = "https://secureacceptance.cybersource.com/silent/pay";
            }
            
            return checkoutPaymentUrl;
        }

        /// <summary>
        /// Please change value based on test/prod in query string
        /// </summary>
        /// <param name="isTest"></param>        
        private CaptureContextRequestModel GenerateCaptureContext(bool isTest)
        {
            CaptureContextRequestModel captureContextPayload = new CaptureContextRequestModel();
            if (isTest)
            {
                captureContextPayload.checkoutApiInitialization.profile_id = "273DF0FB-8160-4D30-8B5D-A0A2C029498A";
                captureContextPayload.checkoutApiInitialization.access_key = "79f59f96b7343a4dad2bf998318648c3";
                captureContextPayload.checkoutApiInitialization.partner_solution_id = "IJUZV2FD";

                captureContextPayload.checkoutApiInitialization.reference_number = "2632497875015"; //unique order number
                captureContextPayload.checkoutApiInitialization.transaction_uuid = "26173845732-003";
                captureContextPayload.checkoutApiInitialization.amount = "101.05";
                captureContextPayload.checkoutApiInitialization.currency = "GBP";

                //this weebhook this not develop it is recommed to develop it to resend the transection details at back end as notification
                captureContextPayload.checkoutApiInitialization.override_backoffice_post_url = "https://webhook.site/aef5e955-9a59-44b3-a186-f9a42d23181a";

                captureContextPayload.checkoutApiInitialization.override_custom_receipt_page = string.Format("{0}", HttpContext.Current.Request.Url.AbsoluteUri);
                captureContextPayload.targetOrigins.Add("https://localhost:44380");

                captureContextPayload.checkoutApiInitialization.PaymentMethod = "card";
                captureContextPayload.checkoutApiInitialization.currency = "GBP";
                captureContextPayload.checkoutApiInitialization.locale = "en";
                captureContextPayload.checkoutApiInitialization.ignore_avs = "true";
                captureContextPayload.checkoutApiInitialization.ignore_cvn = "true";
                captureContextPayload.checkoutApiInitialization.payer_authentication_challenge_code = "04";
                captureContextPayload.checkoutApiInitialization.payer_authentication_transaction_mode = "S";
                captureContextPayload.checkoutApiInitialization.unsigned_field_names = "transient_token";
            }
            else
            {
                captureContextPayload.checkoutApiInitialization.profile_id = "";
                captureContextPayload.checkoutApiInitialization.access_key = "";
                captureContextPayload.checkoutApiInitialization.partner_solution_id = "";

                captureContextPayload.checkoutApiInitialization.reference_number = ""; //unique order number
                captureContextPayload.checkoutApiInitialization.transaction_uuid = "";
                bool isFirstTimeSubscriber = false;
                captureContextPayload.checkoutApiInitialization.transaction_type = isFirstTimeSubscriber ? "sale,create_payment_token" : "sale";
                captureContextPayload.checkoutApiInitialization.amount = "101.05";
                captureContextPayload.checkoutApiInitialization.currency = "GBP";

                //this weebhook this not develop it is recommed to develop it to resend the transection details at back end as notification
                captureContextPayload.checkoutApiInitialization.override_backoffice_post_url = "";

                //note that we are passing Mode query string to be used in call which will come from cybersource
                //same query string we are using to initate the mode test please do not change the query string name
                //as when calling from cybercorce it create a new instance of page which we have hooked to return to calling page instance 
                captureContextPayload.checkoutApiInitialization.override_custom_receipt_page = string.Format("{0}", HttpContext.Current.Request.Url.AbsoluteUri);
                captureContextPayload.targetOrigins.Add("");

                captureContextPayload.checkoutApiInitialization.PaymentMethod = "";
                captureContextPayload.checkoutApiInitialization.currency = "";
                captureContextPayload.checkoutApiInitialization.locale = "";
                captureContextPayload.checkoutApiInitialization.ignore_avs = "";
                captureContextPayload.checkoutApiInitialization.ignore_cvn = "";
                captureContextPayload.checkoutApiInitialization.payer_authentication_challenge_code = "";
                captureContextPayload.checkoutApiInitialization.payer_authentication_transaction_mode = "";

                captureContextPayload.checkoutApiInitialization.unsigned_field_names = "transient_token";
            }

            return captureContextPayload;
        }

        /// <summary>
        /// Please change value based on test/prod in query string
        /// </summary>
        /// <param name="isTest"></param>
        private CallAPIConfigModel GenerateAPIConfigPayload(bool isTest)
        {
            CallAPIConfigModel callAPIConfig = new CallAPIConfigModel();

            if (isTest)
            {
                //these details can be taken from shared secret key section in ECB portal
                callAPIConfig.merchantID = "cwpdbookingonlinetestgbpe";
                callAPIConfig.merchantsharedsecretKey = "bNPpMhYh5VpAsuTDn8HceNPRB9d0FPGLGwxSqRO8RIQ=";
                callAPIConfig.merchantKeyId = "0db39dfd-e509-407e-8473-37707abdf03d";

                //idealy should be picked from application config.
                //do not remove the forward and backword slash from start and end.
                callAPIConfig.requestHost = "apitest.cybersource.com";
                callAPIConfig.captureContextResource = "/microform/v2/sessions";
                callAPIConfig.validateContextResource = "/flex/v2/public-keys/";
                callAPIConfig.mitPaymentResource = "/pts/v2/payments";
            }
            else
            {
                //these details can be taken from shared secret key section in ECB portal
                callAPIConfig.merchantID = "";
                callAPIConfig.merchantsharedsecretKey = "";
                callAPIConfig.merchantKeyId = "";

                //idealy should be picked from application config.
                //do not remove the forward and backword slash from start and end.
                callAPIConfig.requestHost = "api.cybersource.com";
                callAPIConfig.captureContextResource = "/microform/v2/sessions";
                callAPIConfig.validateContextResource = "/flex/v2/public-keys/";
                callAPIConfig.mitPaymentResource = "/pts/v2/payments";
            }

            return callAPIConfig;
        }

        /// <summary>
        /// Please change value based on test, profile is needed here as this call will only happen when cybersouce api will call us
        /// please note calling from cybercorce will create a new instance of page which we have hooked to return to calling page instance 
        /// </summary>
        /// <param name="isTest"></param>
        /// <param name="profileId">currently profile id is not use but should be use in real time</param>
        /// <returns></returns>
        private string GenerateSecurityKey(bool isTest, string profileId)
        {
            //this is profile key generated in security section in ECB
            string SECRET_KEY = string.Empty;
            if (isTest)
            {
                SECRET_KEY = "627ca8fb6576484a9cd3324d2bddb762fb23be7953144f69824339bfeedc9d6dd6422675083046898f1ea0d7b11128803643931325a44743981adbce030cad4a4d95eb019f51493fbeb94f5e9c69fadd852032215cc24ec49f531d6e71351972b3474e208d9b41b1beccbf616fbdf682fe1f5c7d6fb645b4ac1901dd87194f34";
            }
            else
            {
                SECRET_KEY = ""; //profileId get security key based on profile id from DB/secure valt
            }

            return SECRET_KEY;
        }

        #region Autopay - to be implemented in diffrent class only for demo perpose in this page
        //implemented for demo perpose should be done
        private void SubscribeAutoPay(string savedToken, string currency, string totalAmount)
        {            
            CallAPIConfigModel callApiConfig = JsonConvert.DeserializeObject<CallAPIConfigModel>(Convert.ToString(ViewState["CallApiConfig"]));
            string response = CallCybersourceAPI.CallMITUnscheduled(GenerateMITUnscheduled(savedToken, currency, totalAmount), callApiConfig);
            MITResponseModel mitResponse = JsonConvert.DeserializeObject<MITResponseModel>(response);

            if (mitResponse.status == "AUTHORIZED")
            {
                //process success payment
            }
            else
            {
                //process failure 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private MITRequestModel GenerateMITUnscheduled(string savedToken, string currency, string totalAmount)
        {
            MITRequestModel mitRequest = new MITRequestModel();
            mitRequest.clientReferenceInformation.code = "UniqueIdentificationTransaction";
            mitRequest.paymentInformation.paymentInstrument.id = savedToken; // token recived from create pament call
            mitRequest.orderInformation.amountDetails.currency = currency;
            mitRequest.orderInformation.amountDetails.totalAmount = totalAmount;
            mitRequest.processingInformation.capture = "true";
            mitRequest.processingInformation.commerceIndicator = "internet";
            mitRequest.processingInformation.authorizationOptions.ignoreAvsResult = "true";
            mitRequest.processingInformation.authorizationOptions.ignoreCvResult = "true";
            mitRequest.processingInformation.authorizationOptions.initiator.storedCredentialUsed = "true";
            mitRequest.processingInformation.authorizationOptions.initiator.type = "merchant";

            return mitRequest;
        }

        #endregion
    }
}