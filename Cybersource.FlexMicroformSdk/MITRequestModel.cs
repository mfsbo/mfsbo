using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cybersource.FlexMicroformSdk
{
    public class Initiator
    {
        public string type { get; set; }
        public string storedCredentialUsed { get; set; }
    }
    public class AuthorizationOptions
    {
        public string ignoreAvsResult { get; set; }
        public string ignoreCvResult { get; set; }
        public Initiator initiator { get; set; }

        public AuthorizationOptions()
        {
            this.initiator = new Initiator();
        }
    } 

    public class ProcessingInformation
    {
        public string capture { get; set; }
        public string commerceIndicator { get; set; }
        public AuthorizationOptions authorizationOptions { get; set; }

        public ProcessingInformation()
        {
            this.authorizationOptions = new AuthorizationOptions();
        }
    }

    public class MITRequestModel
    {
        public ClientReferenceInformation clientReferenceInformation { get; set; }
        public PaymentInformation paymentInformation { get; set; }
        public OrderInformation orderInformation { get; set; }
        public ProcessingInformation processingInformation { get; set; }

        public MITRequestModel()
        {
            this.clientReferenceInformation = new ClientReferenceInformation(); 
            this.paymentInformation = new PaymentInformation();
            this.orderInformation = new OrderInformation();
            this.processingInformation = new ProcessingInformation();
        }
    }


}