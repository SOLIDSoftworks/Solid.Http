using Solid.Http.Zip.Abstraction;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace Solid.Http.Zip.Providers
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
