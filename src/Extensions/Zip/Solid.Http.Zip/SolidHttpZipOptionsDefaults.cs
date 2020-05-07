using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;

namespace Solid.Http.Zip
{
    public static class SolidHttpZipOptionsDefaults
    {
        public static readonly ZipArchiveMode Mode = ZipArchiveMode.Read;

        public static List<MediaTypeHeaderValue> SupportedMediaTypes => CreateDefaultSupportedMediaTypes();

        private static List<MediaTypeHeaderValue> CreateDefaultSupportedMediaTypes()
        {
            return new List<MediaTypeHeaderValue>
            {
                MediaTypeHeaderValue.Parse("application/zip")
            };
        }
    }
}
