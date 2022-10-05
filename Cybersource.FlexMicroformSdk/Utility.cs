using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;

namespace Cybersource.FlexMicroformSdk
{
    public static class Utility
    {
        public static string MyDictionaryToJson(IDictionary<string, string> dict)
        {
            var entries = dict.Select(d =>
                string.Format("\"{0}\": \"{1}\"", d.Key, d.Value));
            
            return "{" + string.Join(",", entries) + "}";
        }

        public static Dictionary<string, string> ReadEmbededLanguageFile()
        {
            Dictionary<string, string> VALUES_LIST = new Dictionary<string, string>();

            ResourceManager resmanager = new ResourceManager("Cybersource.FlexMicroformSdk.ReasonCode", Assembly.GetExecutingAssembly());
            ResourceSet resourceSet = resmanager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                string resourceKey = entry.Key.ToString();
                string resource = entry.Value.ToString();

                VALUES_LIST.Add(resourceKey, resource);
            }

            return VALUES_LIST;
        }
    }
}