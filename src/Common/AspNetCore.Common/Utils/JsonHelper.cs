using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace AspNetCore.Common.Utils
{
    public static class JsonHelper
    {
        public static void JsonWriter(string source, string nameJson, string output)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\JsonApi\\" + source + "\\" + nameJson;
            if (!File.Exists(path))
                path = string.Format("{0}\\JsonApi\\{1}\\{2}", Directory.GetCurrentDirectory(), source, nameJson);
            if (File.Exists(path))
            {
                File.WriteAllText(path, output, Encoding.UTF8);
            }
        }
        public static T? JsonReader<T>(string source, string nameJson) where T : new()
        {
            string json = string.Empty;
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\JsonApi\\" + source + "\\" + nameJson;
            if (!File.Exists(path))
                path = string.Format("{0}\\JsonApi\\{1}\\{2}", Directory.GetCurrentDirectory(), source, nameJson);
            if (File.Exists(path))
            {
                StreamReader stream = new(path, Encoding.GetEncoding("UTF-8"));
                json = stream.ReadToEnd();
                stream.Close();
            }
            return !string.IsNullOrEmpty(json) ? Deserialize<T>(json) : default;
        }
        public static string JsonReaderString(string source, string nameJson)
        {
            string jsonRead = string.Empty;
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\JsonApi\\" + source + "\\" + nameJson;
            if (!File.Exists(path))
                path = string.Format("{0}\\JsonApi\\{1}\\{2}", Directory.GetCurrentDirectory(), source, nameJson);
            if (File.Exists(path))
            {
                StreamReader stream = new(path, Encoding.GetEncoding("UTF-8"));
                jsonRead = stream.ReadToEnd();

            }
            return jsonRead;
        }
        public static T? Deserialize<T>(string json)
        {
            JsonSerializerSettings settings = new()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
                { NamingStrategy = new CamelCaseNamingStrategy(true, true) }
            };

            return !string.IsNullOrEmpty(json) ? JsonConvert.DeserializeObject<T>(json, settings) : default;
        }
        public static string Serialize<T>(T obj)
        {
            var setting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
                { NamingStrategy = new CamelCaseNamingStrategy(true, true) }
            };
            return JsonConvert.SerializeObject(obj, setting);
        }
        public static object? JsonConvertObject(object data)
        {
            var setting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                ContractResolver = new CamelCasePropertyNamesContractResolver()
                { NamingStrategy = new CamelCaseNamingStrategy(true, true) }
            };
            var json = JsonConvert.SerializeObject(data, setting);
            return !string.IsNullOrEmpty(json) ? JsonConvert.DeserializeObject<object>(json) : default;
        }
    }
}