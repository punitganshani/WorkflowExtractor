using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
namespace WFE.Lib
{
    internal static class Serializer
    {
        private static readonly Dictionary<Type, XmlSerializer> _serializer;
        static Serializer()
        {
            _serializer = new Dictionary<Type, XmlSerializer>();
        }

        private static XmlSerializer GetSerializer<T>()
        {
            Type type = typeof(T);
            if (_serializer.ContainsKey(type))
                return _serializer[type];
            else
            {
                var xs = new XmlSerializer(type);
                _serializer[type] = xs;
                return xs;
            }
        }

        /// <summary>
        /// Serialize an object into an XML string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj, Encoding encoding)
        {
            try
            {

                string xmlString = null;
                var memoryStream = new MemoryStream();
                XmlSerializer xs = GetSerializer<T>();
                var xmlTextWriter = new XmlTextWriter(memoryStream, encoding);
                xs.Serialize(xmlTextWriter, obj);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                xmlString = Converter.ToString(memoryStream.ToArray());
                return xmlString;
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Reconstruct an object from an XML string
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml, Encoding encoding)
        {
            XmlSerializer xs = GetSerializer<T>();
            var memoryStream = new MemoryStream(Converter.ToByte(xml));
            var xmlTextWriter = new XmlTextWriter(memoryStream, encoding);
            return (T)xs.Deserialize(memoryStream);
        }


    }
}
