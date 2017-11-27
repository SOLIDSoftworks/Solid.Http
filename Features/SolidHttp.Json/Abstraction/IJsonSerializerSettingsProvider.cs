using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http.Json.Abstraction
{
    internal interface IJsonSerializerSettingsProvider
    {
        JsonSerializerSettings GetJsonSerializerSettings();
    }
}
