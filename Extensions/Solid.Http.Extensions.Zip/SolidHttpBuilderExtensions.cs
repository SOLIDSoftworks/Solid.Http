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
        /// <para>Can create a deserializer for application/xml and text/xml</para>
        /// </summary>
        /// <param name="builder">The setup</param>
        /// <param name="settings">Supplied XmlSerializerSettings</param>
        /// <returns>ISolidHttpSetup</returns>
        public static ISolidHttpBuilder AddZip(this ISolidHttpBuilder builder, ZipArchiveMode mode = ZipArchiveMode.Read)
        {
            var provider = new ZipArchiveSerializerSettingsProvider(mode);
            builder.Services.AddSingleton<IZipSerializerSettingsProvider>(provider);
            builder.AddDeserializer<ZipArchiveResponseDeserializerFactory>("application/zip", "application/octet-stream");

            return builder;
        }

        /// <summary>
        /// Adds support for ZipArchive
        /// <para>Can create a deserializer for application/xml and text/xml</para>
        /// </summary>
        /// <param name="builder">The setup</param>
        /// <param name="settings">Supplied XmlSerializerSettings</param>
        /// <returns>ISolidHttpSetup</returns>
        public static ISolidHttpCoreBuilder AddZip(this ISolidHttpCoreBuilder builder, ZipArchiveMode mode = ZipArchiveMode.Read)
        {
            var provider = new ZipArchiveSerializerSettingsProvider(mode);
            builder.Services.AddSingleton<IZipSerializerSettingsProvider>(provider);
            builder.AddDeserializer<ZipArchiveResponseDeserializerFactory>("application/zip", "application/octet-stream");

            return builder;
        }
    }
}
