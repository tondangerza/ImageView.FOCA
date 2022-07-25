using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ImageView
{
    public static class ClassSaving
    {
        public static bool SaveClass(object obj, string path)
        {
            try
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter F;
                System.IO.Stream s;
                F = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                s = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
                F.Serialize(s, obj);
                s.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool LoadClass(ref object obj, string path)
        {
            try
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter f;
                System.IO.Stream s;
                f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                s = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None);
                obj = (object)f.Deserialize(s);
                s.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static object DeserializeFromStream(MemoryStream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            object objectType = formatter.Deserialize(stream);
            return objectType;
        }



        public static T XMLDeserialize<T>(string value)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(new StringReader(value));
        }


        public static string XMLSerialize<T>(T value, Encoding enconding = null)
        {
            if (value == null)
                return string.Empty;
            var xmlSerializer = new XmlSerializer(typeof(T));
            XmlWriterSettings xmlSetting = new XmlWriterSettings() { Indent = true };
            using (var stringWriter = new StringWriterWithEncoding(enconding))
            {
                using (var _xmlWriter = XmlWriter.Create(stringWriter, xmlSetting))
                {
                    xmlSerializer.Serialize(_xmlWriter, value);
                    return stringWriter.ToString();
                }
            }
        }

        public sealed class StringWriterWithEncoding : StringWriter
        {
            public override Encoding Encoding { get; }

            public StringWriterWithEncoding(Encoding encoding = null)
            {
                if (encoding != null)
                    this.Encoding = encoding;
            }
        }
    }
}
