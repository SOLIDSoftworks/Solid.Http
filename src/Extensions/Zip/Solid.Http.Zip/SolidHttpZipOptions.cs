using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Net.Http.Headers;

namespace Solid.Http.Zip
{
    /// <summary>
    /// Options for configuring Solid.Http.Zip.
    /// </summary>
    public class SolidHttpZipOptions
    {
        /// <summary>
        /// The <see cref="ZipArchiveMode" /> used when instatiating a <seealso cref="ZipArchive" />.
        /// </summary>
        public ZipArchiveMode Mode { get; set; } = SolidHttpZipOptionsDefaults.Mode;

        /// <summary>
        /// The supported ZIP media types.
        /// </summary>
        public List<MediaTypeHeaderValue> SupportedMediaTypes = SolidHttpZipOptionsDefaults.SupportedMediaTypes;
    }
}
