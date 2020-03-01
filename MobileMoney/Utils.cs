using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMoney
{
    public static class Utils
    {
        public static string Serialize(object value)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });

            return JsonConvert.SerializeObject(value, settings);
        }

        public static T Deserialize<T>(string value)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });

            return JsonConvert.DeserializeObject<T>(value, settings);
        }

        public static string GetEncodedBasicAuth(string userId, string apiKey)
        {
            var value = $"{userId}:{apiKey}";

            byte[] data = Encoding.ASCII.GetBytes(value);
            var base64Encoded = Convert.ToBase64String(data);


            return base64Encoded;
        }
    }
}
