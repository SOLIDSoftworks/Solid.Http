using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Net.Http.Headers;

namespace Solid.Http.Zip
{
    public class SolidHttpZipOptions
    {
        public ZipArchiveMode Mode { get; set; } = SolidHttpZipOptionsDefaults.Mode;
        public List<MediaTypeHeaderValue> SupportedMediaTypes = SolidHttpZipOptionsDefaults.SupportedMediaTypes;
    }
}
