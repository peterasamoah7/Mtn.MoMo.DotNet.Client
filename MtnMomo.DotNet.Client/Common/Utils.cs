using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace MtnMomo.DotNet.Client.Common
{
    public static class Utils
    {
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Serialize(object value)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter(typeof(CamelCaseNamingStrategy)));

            return JsonConvert.SerializeObject(value, settings);
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string value)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter(typeof(CamelCaseNamingStrategy)));

            return JsonConvert.DeserializeObject<T>(value, settings);
        }

        /// <summary>
        /// Create Basic Auth for Token API Calls
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static string GetEncodedBasicAuth(string userId, string apiKey)
        {
            var value = $"{userId}:{apiKey}";

            byte[] data = Encoding.ASCII.GetBytes(value);
            var base64Encoded = Convert.ToBase64String(data);


            return base64Encoded;
        }
    }
}
