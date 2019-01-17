using Solid.Http.Extenstions.Zip.Abstraction;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace Solid.Http.Extenstions.Zip.Providers
{
    internal class ZipArchiveSerializerSettingsProvider : IZipSerializerSettingsProvider
    {
        private static ZipArchiveMode _mode;

        public ZipArchiveSerializerSettingsProvider(ZipArchiveMode mode)
        {
            _mode = mode;
        }
        public ZipArchiveMode GetZipSerializerSettings()
        {
            return _mode;
        }
    }
}
