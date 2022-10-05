using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cybersource.FlexMicroformSdk
{
    public class AmountDetails
    {
        public string totalAmount { get; set; }
        public string authorizedAmount { get; set; }
        public string currency { get; set; }
    }

    public class Avs
    {
        public string code { get; set; }
        public string codeRaw { get; set; }
    }

    public class Card
    {
        public string type { get; set; }
    }

    public class ClientReferenceInformation
    {
        public string code { get; set; }
    }

    public class ConsumerAuthenticationInformation
    {
        public string token { get; set; }
    }

    public class InfoCodes
    {
        public List<string> address { get; set; }
        public List<string> phone { get; set; }
        public List<string> globalVelocity { get; set; }
        public List<string> suspicious { get; set; }
        public List<string> identityChange { get; set; }
    }

    public class InstrumentIdentifier
    {
        public string id { get; set; }
        public string state { get; set; }
    }

    public class Links
    {
        public Void @void { get; set; }
        public Self self { get; set; }
    }

    public class OrderInformation
    {
        public AmountDetails amountDetails { get; set; }

        public OrderInformation()
        {
            this.amountDetails = new AmountDetails();
        }
    }

    public class PaymentAccountInformation
    {
        public Card card { get; set; }
    }

    public class PaymentInformation
    {
        public TokenizedCard tokenizedCard { get; set; }
        public string scheme { get; set; }
        public InstrumentIdentifier instrumentIdentifier { get; set; }
        public string bin { get; set; }
        public PaymentInstrument paymentInstrument { get; set; }
        public string issuer { get; set; }
        public Card card { get; set; }
        public string binCountry { get; set; }

        public PaymentInformation()
        {
            this.paymentInstrument = new PaymentInstrument();
        }
    }

    public class PaymentInstrument
    {
        public string id { get; set; }
    }

    public class PointOfSaleInformation
    {
        public string terminalId { get; set; }
    }

    public class ProcessorInformation
    {
        public string merchantNumber { get; set; }
        public string approvalCode { get; set; }
        public string responseCode { get; set; }
        public Avs avs { get; set; }
    }

    public class Profile
    {
        public string earlyDecision { get; set; }
        public string name { get; set; }
        public string selectorRule { get; set; }
    }

    public class RiskInformation
    {
        public string localTime { get; set; }
        public Score score { get; set; }
        public InfoCodes infoCodes { get; set; }
        public Profile profile { get; set; }
    }

    public class MITResponseModel
    {
        public Links _links { get; set; }
        public ClientReferenceInformation clientReferenceInformation { get; set; }
        public ConsumerAuthenticationInformation consumerAuthenticationInformation { get; set; }
        public string id { get; set; }
        public OrderInformation orderInformation { get; set; }
        public PaymentAccountInformation paymentAccountInformation { get; set; }
        public PaymentInformation paymentInformation { get; set; }
        public PointOfSaleInformation pointOfSaleInformation { get; set; }
        public ProcessorInformation processorInformation { get; set; }
        public string reconciliationId { get; set; }
        public RiskInformation riskInformation { get; set; }
        public string status { get; set; }
        public DateTime submitTimeUtc { get; set; }
    }

    public class Score
    {
        public string result { get; set; }
        public List<string> factorCodes { get; set; }
        public string modelUsed { get; set; }
    }

    public class Self
    {
        public string method { get; set; }
        public string href { get; set; }
    }

    public class TokenizedCard
    {
        public string type { get; set; }
    }

    public class Void
    {
        public string method { get; set; }
        public string href { get; set; }
    }
}