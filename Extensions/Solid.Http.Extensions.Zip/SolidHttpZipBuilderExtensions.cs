using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using Solid.Http.Zip;
using Solid.Http.Zip.Abstraction;
using Solid.Http.Zip.Providers;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// SolidHttpZipBuilderExtensions
    /// </summary>
    public static class SolidHttpZipBuilderExtensions
    {
        /// <summary>
        /// Adds support for ZipArchive
        /// <para>Can create a deserializer for application/zip and application/octet-stream</para>
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="mode">The zip archive mode</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder AddZip(this ISolidHttpBuilder builder, ZipArchiveMode mode = ZipArchiveMode.Read)
        {
            var provider = new ZipArchiveSerializerSettingsProvider(mode);
            builder.Services.AddSingleton<IZipSerializerSettingsProvider>(provider);
            builder.AddDeserializer<ZipArchiveResponseDeserializerFactory>("application/zip", "application/octet-stream");
            return builder;
        }

        /// <summary>
        /// Adds support for ZipArchive
        /// <para>Can create a deserializer for application/zip and application/octet-stream</para>
        /// </summary>
        /// <param name="builder">The setup</param>
        /// <param name="mode">The zip archive mode</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpCoreBuilder AddZip(this ISolidHttpCoreBuilder builder, ZipArchiveMode mode = ZipArchiveMode.Read)
            => ((ISolidHttpBuilder)builder).AddZip(mode) as ISolidHttpCoreBuilder;
    }
}
