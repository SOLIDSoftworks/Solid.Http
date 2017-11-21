using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Json.Abstraction
{
    internal interface IJsonSerializerSettingsProvider
    {
        JsonSerializerSettings GetJsonSerializerSettings();
    }
}
