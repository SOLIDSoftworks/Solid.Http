using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Solid.Http.Extenstions.Xml.Abstraction
{
    internal interface IXmlSerializerSettingsProvider
    {
        DataContractSerializerSettings GetXmlSerializerSettings();
    }
}
