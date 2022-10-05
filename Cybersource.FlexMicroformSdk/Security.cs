using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Specialized;

namespace Cybersource.FlexMicroformSdk
{
    public static class Security
    {
        //private const String SECRET_KEY = "627ca8fb6576484a9cd3324d2bddb762fb23be7953144f69824339bfeedc9d6dd6422675083046898f1ea0d7b11128803643931325a44743981adbce030cad4a4d95eb019f51493fbeb94f5e9c69fadd852032215cc24ec49f531d6e71351972b3474e208d9b41b1beccbf616fbdf682fe1f5c7d6fb645b4ac1901dd87194f34";

        public static String sign(IDictionary<string, string> paramsArray, string secretKey)  {
            return sign(buildDataToSign(paramsArray), secretKey);
        }

        private static String sign(String data, String secretKey) {
            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secretKey);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
        }

        private static String buildDataToSign(IDictionary<string,string> paramsArray) {
            String[] signedFieldNames = paramsArray["signed_field_names"].Split(',');
            IList<string> dataToSign = new List<string>();

	        foreach (String signedFieldName in signedFieldNames)
	        {
	             dataToSign.Add(signedFieldName + "=" + paramsArray[signedFieldName]);
	        }

            return commaSeparate(dataToSign);
        }
        private static String commaSeparate(IList<string> dataToSign) {
            return String.Join(",", dataToSign);                         
        }
    }
}
