using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cybersource.FlexMicroformSdk
{
    public class CallAPIConfigModel
    {
        // Try with your own credentaials
        // Get Key ID, Shared Secret Key and Merchant Id from EBC portal
        public string merchantID { get; set; } //"cwpdbookingonlinetestgbpe";
        public string merchantKeyId { get; set; }// "0db39dfd-e509-407e-8473-37707abdf03d";
        public string merchantsharedsecretKey { get; set; }// "bNPpMhYh5VpAsuTDn8HceNPRB9d0FPGLGwxSqRO8RIQ=";
        public string requestHost { get; set; }// "apitest.cybersource.com";
        public string captureContextResource { get; set; }  //"/microform/v2/sessions";
        public string validateContextResource { get; set; }  //""/flex/v2/public-keys/"";
        public string mitPaymentResource { get; set; }  //""/pts/v2/payments"";
    }
}