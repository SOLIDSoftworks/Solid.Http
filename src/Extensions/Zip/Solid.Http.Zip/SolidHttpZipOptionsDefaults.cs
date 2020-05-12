using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;

namespace Solid.Http.Zip
{
    /// <summary>
    /// Default values for <see cref="SolidHttpZipOptions" />.
    /// </summary>
    public static class SolidHttpZipOptionsDefaults
    {
        /// <summary>
        /// Default <see cref="ZipArchiveMode" />.
        /// </summary>
        public static readonly ZipArchiveMode Mode = ZipArchiveMode.Read;

        /// <summary>
        /// Default supported ZIP media types.
        /// </summary>
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
