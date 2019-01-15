
using Solid.Http.Abstractions;
using Solid.Http.Xml.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Xml
{
    internal class XmlResponseDeserializerFactory : IResponseDeserializerFactory
    {
        public XmlResponseDeserializerFactory(IXmlSerializerSettingsProvider provider)
        {
            GetSettings = () => provider.GetXmlSerializerSettings();
        }

        internal XmlResponseDeserializerFactory(DataContractSerializerSettings settings)
        {
            GetSettings = () => settings;
        }

        private Func<DataContractSerializerSettings> GetSettings { get; }
        public Func<HttpContent, Task<T>> CreateDeserializer<T>()
        {
            return async (content) =>
            {
                using (var ms = new MemoryStream())
                {
                    await content.CopyToAsync(ms);
                    ms.Position = 0;
                    var ser = new DataContractSerializer(typeof(T), GetSettings());
                    return (T)ser.ReadObject(ms);
                }
            };
        }
    }
}
