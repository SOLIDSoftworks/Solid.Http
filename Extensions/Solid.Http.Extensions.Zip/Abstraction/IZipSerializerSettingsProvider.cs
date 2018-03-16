using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace Solid.Http.Zip.Abstraction
{
    internal interface IZipSerializerSettingsProvider
    {
         ZipArchiveMode GetZipSerializerSettings();
    }
}
