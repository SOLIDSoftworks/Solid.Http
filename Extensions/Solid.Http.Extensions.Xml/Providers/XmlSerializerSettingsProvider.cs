using Solid.Http.Extenstions.Xml.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Solid.Http.Extenstions.Xml.Providers
{
    internal class XmlSerializerSettingsProvider : IXmlSerializerSettingsProvider
    {
        private static DataContractSerializerSettings _settings;

        public XmlSerializerSettingsProvider(DataContractSerializerSettings settings)
        {
            _settings = settings;
        }
        public DataContractSerializerSettings GetXmlSerializerSettings()
        {
            return _settings;
        }
    }
}
