using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AspNetCore.Common.Helpers
{
    public static class XmlHelper
    {
        public const string XmlHeaderUnicode = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";

        /// <summary>
        /// Deserialize using XmlSerializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T? DeserializeXml<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            StringReader stringReader = new StringReader(xml);
            using var xmlReader = new XmlTextReader(stringReader);
            return (T?)serializer.Deserialize(xmlReader);
        }

        /// <summary>
        /// Serialize using XmlSerializer
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeXml(object obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            string result;
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                xmlSerializer.Serialize(stringWriter, obj);
                stringWriter.Flush();
                result = stringWriter.ToString();
            }

            return result;
        }
    }
}
