using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using Solid.Http.Zip.Abstraction;
using Solid.Http.Zip.Providers;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace Solid.Http.Zip
{
    public static class SolidHttpBuilderExtensions
    {
        /// <summary>
        /// Adds support for ZipArchive
        /// <para>Can create a deserializer for supplied mime types for zip archives</para>
        /// </summary>
        /// <param name="builder">The setup</param>
        /// <param name="mode">ZipArhive mode</param>
        /// <param name="mimeTypes">MimeTypes for the deserializer</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder AddZip(this ISolidHttpBuilder builder, ZipArchiveMode mode = ZipArchiveMode.Read, params string[] additionalMimeTypes )
        {
            var provider = new ZipArchiveSerializerSettingsProvider(mode);
            builder.Services.AddSingleton<IZipSerializerSettingsProvider>(provider);
            builder.Services.AddSolidHttpDeserializer<ZipArchiveResponseDeserializerFactory>("application/zip", additionalMimeTypes);

            return builder;
        }


        /// <summary>
        /// Adds support for ZipArchive
        /// <para>Can create a deserializer for supplied mime types for zip archives</para>
        /// </summary>
        /// <param name="builder">The setup</param>
        /// <param name="mode">ZipArhive mode</param>
        /// <param name="additionalMimeTypes">Additional MimeTypes for the deserializer, the default one is application/zip</param>
        /// <returns>ISolidHttpCoreBuilder</returns>
        public static ISolidHttpCoreBuilder AddZip(this ISolidHttpCoreBuilder builder, ZipArchiveMode mode = ZipArchiveMode.Read, params string[] additionalMimeTypes)
        {
            var provider = new ZipArchiveSerializerSettingsProvider(mode);
            builder.Services.AddSingleton<IZipSerializerSettingsProvider>(provider);
            builder.Services.AddSolidHttpDeserializer<ZipArchiveResponseDeserializerFactory>("application/zip", additionalMimeTypes);

            return builder;
        }
    }
}
