using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace Cybersource.FlexMicroformSdk
{
    public class CallCybersourceAPI
    {    

        /// <summary>
        /// This demonstrates what a generic API request helper method would look like.
        /// </summary>
        /// <param name="request">Request to send to API endpoint</param>
        /// <returns>Task</returns>
        public static string CallCaptureContext(CaptureContextRequestModel captureContectPayload ,CallAPIConfigModel callApiConfig)
        {
            string request = JsonConvert.SerializeObject(captureContectPayload);
            string responsecontent = String.Empty;
            // HTTP POST request
            using (var client = new HttpClient())
            {
                string resource = callApiConfig.captureContextResource;
                StringContent content = new StringContent(request);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); // content-type header

                /* Add Request Header :: "v-c-merchant-id" set value to Cybersource Merchant ID or v-c-merchant-id
                 * This ID can be found on EBC portal
                 */
                client.DefaultRequestHeaders.Add("v-c-merchant-id", callApiConfig.merchantID);

                /* Add Request Header :: "Date" The date and time that the message was originated from.
                 * "HTTP-date" format as defined by RFC7231.
                 */
                string gmtDateTime = DateTime.Now.ToUniversalTime().ToString("r");
                client.DefaultRequestHeaders.Add("Date", gmtDateTime);

                /* Add Request Header :: "Host"
                 * Sandbox Host: apitest.cybersource.com
                 * Production Host: api.cybersource.com
                 */
                client.DefaultRequestHeaders.Add("Host", callApiConfig.requestHost);

                /* Add Request Header :: "Digest"
                 * Digest is SHA-256 hash of payload that is BASE64 encoded
                 */
                var digest = GenerateDigest(request);
                client.DefaultRequestHeaders.Add("Digest", digest);

                /* Add Request Header :: "Signature"
                 * Signature header contains keyId, algorithm, headers and signature as paramters
                 * method getSignatureHeader() has more details
                 */
                StringBuilder signature = GenerateSignature(request, digest, string.Empty, gmtDateTime, "post", resource, callApiConfig);
                client.DefaultRequestHeaders.Add("Signature", signature.ToString());



                var response = client.PostAsync("https://" + callApiConfig.requestHost + resource, content).Result;
                responsecontent = response.Content.ReadAsStringAsync().Result;
            }

            return responsecontent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool ValidateCaptureContext(string captureContext, CallAPIConfigModel callApiConfig)
        {
            var stream = captureContext;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;

            string kid = "zu";
            if (tokenS.Header.ContainsKey("kid"))
                kid = tokenS.Header["kid"].ToString();

            string validatecontentModulus = String.Empty;
            string validatecontentExponent = string.Empty;
            string validatecontent = string.Empty;
            // HTTP POST request
            using (var client = new HttpClient())
            {
                string resource = callApiConfig.validateContextResource + kid;

                /* Add Request Header :: "v-c-merchant-id" set value to Cybersource Merchant ID or v-c-merchant-id
                 * This ID can be found on EBC portal.
                 */
                client.DefaultRequestHeaders.Add("v-c-merchant-id", callApiConfig.merchantID);

                /* Add Request Header :: "Date" The date and time that the message was originated from.
                 * "HTTP-date" format as defined by RFC7231.
                 */
                string gmtDateTime = DateTime.Now.ToUniversalTime().ToString("r");
                client.DefaultRequestHeaders.Add("Date", gmtDateTime);

                /* Add Request Header :: "Host"
                 * Sandbox Host: apitest.cybersource.com
                 * Production Host: api.cybersource.com
                 */
                client.DefaultRequestHeaders.Add("Host", callApiConfig.requestHost);

                /* Add Request Header :: "Signature"
                 * Signature header contains keyId, algorithm, headers and signature as paramters
                 * method getSignatureHeader() has more details
                 */
                StringBuilder signature = GenerateSignature(captureContext, string.Empty, string.Empty, gmtDateTime, "get", resource, callApiConfig);
                client.DefaultRequestHeaders.Add("Signature", signature.ToString());


                using (var r = client.GetAsync(new Uri("https://" + callApiConfig.requestHost + resource)).Result)
                {
                    string result = r.Content.ReadAsStringAsync().Result;
                    JObject json = JObject.Parse(result);
                    if (json.ContainsKey("n"))
                        validatecontentModulus = json["n"].ToString();
                    if (json.ContainsKey("e"))
                        validatecontentExponent = json["e"].ToString();
                }

                string tokenStr = captureContext;
                string[] tokenParts = tokenStr.Split('.');

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(
                  new RSAParameters()
                  {
                      Modulus = FromBase64Url(validatecontentModulus),
                      Exponent = FromBase64Url(validatecontentExponent)
                  });

                SHA256 sha256 = SHA256.Create();
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(tokenParts[0] + '.' + tokenParts[1]));

                RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA256");
                if (rsaDeformatter.VerifySignature(hash, FromBase64Url(tokenParts[2])))
                    return true;

            }

            return false;
        }



        public static string CallMITUnscheduled(MITRequestModel mitPaylaod, CallAPIConfigModel callApiConfig)
        {
            string request  = JsonConvert.SerializeObject(mitPaylaod);
            string responsecontent = String.Empty;
            // HTTP POST request
            using (var client = new HttpClient())
            {
                string resource = callApiConfig.mitPaymentResource;
                StringContent content = new StringContent(request);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); // content-type header

                /* Add Request Header :: "v-c-merchant-id" set value to Cybersource Merchant ID or v-c-merchant-id
                 * This ID can be found on EBC portal
                 */
                client.DefaultRequestHeaders.Add("v-c-merchant-id", callApiConfig.merchantID);

                /* Add Request Header :: "Date" The date and time that the message was originated from.
                 * "HTTP-date" format as defined by RFC7231.
                 */
                string gmtDateTime = DateTime.Now.ToUniversalTime().ToString("r");
                client.DefaultRequestHeaders.Add("Date", gmtDateTime);

                /* Add Request Header :: "Host"
                 * Sandbox Host: apitest.cybersource.com
                 * Production Host: api.cybersource.com
                 */
                client.DefaultRequestHeaders.Add("Host", callApiConfig.requestHost);

                /* Add Request Header :: "Digest"
                 * Digest is SHA-256 hash of payload that is BASE64 encoded
                 */
                var digest = GenerateDigest(request);
                client.DefaultRequestHeaders.Add("Digest", digest);

                /* Add Request Header :: "Signature"
                 * Signature header contains keyId, algorithm, headers and signature as paramters
                 * method getSignatureHeader() has more details
                 */
                StringBuilder signature = GenerateSignature(request, digest, string.Empty, gmtDateTime, "post", resource, callApiConfig);
                client.DefaultRequestHeaders.Add("Signature", signature.ToString());

                var response = client.PostAsync("https://" + callApiConfig.requestHost + resource, content).Result;
                responsecontent = response.Content.ReadAsStringAsync().Result;
            }

            return responsecontent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64Url"></param>
        /// <returns></returns>
        static byte[] FromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/")
                                  .Replace("-", "+");
            return Convert.FromBase64String(base64);
        }
        /// <summary>
        /// This method return Digest value which is SHA-256 hash of payload that is BASE64 encoded
        /// </summary>
        /// <param name="request">Value from which to generate digest</param>
        /// <returns>String referring to a digest</returns>
        public static string GenerateDigest(string request)
        {
            string digest = "DIGEST_PLACEHOLDER";


            // Generate the Digest
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] payloadBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(request));
                digest = Convert.ToBase64String(payloadBytes);
                digest = "SHA-256=" + digest;
            }


            return digest;
        }

        // This method returns value for paramter Signature which is then passed to Signature header
        // paramter 'Signature' is calucated based on below key values and then signed with SECRET KEY -
        // host: Sandbox (apitest.cybersource.com) or Production (api.cybersource.com) hostname
        // date: "HTTP-date" format as defined by RFC7231.
        // (request-target): Should be in format of httpMethod: path
        //    Example: "post /pts/v2/payments"
        // Digest: Only needed for POST calls.
        //    digestString = BASE64( HMAC-SHA256 ( Payload ));
        //    Digest: “SHA-256=“ + digestString;
        // v-c-merchant-id: set value to Cybersource Merchant ID
        //    This ID can be found on EBC portal
        public static StringBuilder GenerateSignature(string request, string digest, string keyid, string gmtDateTime, string method, string resource, CallAPIConfigModel callApiConfig)
        {
            StringBuilder signatureHeaderValue = new StringBuilder();
            string algorithm = "HmacSHA256";
            string postHeaders = "host date (request-target) digest v-c-merchant-id";
            string getHeaders = "host date (request-target) v-c-merchant-id";
            string url = "https://" + callApiConfig.requestHost + resource;
            string getRequestTarget = method + " " + resource;
            string postRequestTarget = method + " " + resource;

            // Generate the Signature
            StringBuilder signatureString = new StringBuilder();
            signatureString.Append('\n');
            signatureString.Append("host");
            signatureString.Append(": ");
            signatureString.Append(callApiConfig.requestHost);
            signatureString.Append('\n');
            signatureString.Append("date");
            signatureString.Append(": ");
            signatureString.Append(gmtDateTime);
            signatureString.Append('\n');
            signatureString.Append("(request-target)");
            signatureString.Append(": ");

            if (method.Equals("post"))
            {
                signatureString.Append(postRequestTarget);
                signatureString.Append('\n');
                signatureString.Append("digest");
                signatureString.Append(": ");
                signatureString.Append(digest);
            }
            else
            {
                signatureString.Append(getRequestTarget);
            }

            signatureString.Append('\n');
            signatureString.Append("v-c-merchant-id");
            signatureString.Append(": ");
            signatureString.Append(callApiConfig.merchantID);
            signatureString.Remove(0, 1);

            byte[] signatureByteString = Encoding.UTF8.GetBytes(signatureString.ToString());

            byte[] decodedKey = Convert.FromBase64String(callApiConfig.merchantsharedsecretKey);

            HMACSHA256 aKeyId = new HMACSHA256(decodedKey);

            byte[] hashmessage = aKeyId.ComputeHash(signatureByteString);
            string base64EncodedSignature = Convert.ToBase64String(hashmessage);

            signatureHeaderValue.Append("keyid=\"" + callApiConfig.merchantKeyId + "\"");
            signatureHeaderValue.Append(", algorithm=\"" + algorithm + "\"");

            if (method.Equals("post"))
            {
                signatureHeaderValue.Append(", headers=\"" + postHeaders + "\"");
            }
            else if (method.Equals("get"))
            {
                signatureHeaderValue.Append(", headers=\"" + getHeaders + "\"");
            }

            signatureHeaderValue.Append(", signature=\"" + base64EncodedSignature + "\"");


            return signatureHeaderValue;
        }

        // Converts byte to encrypted string
        public static string ByteToString(byte[] buff)
        {
            string sbinary = string.Empty;

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }

            return sbinary;
        }
    }
}