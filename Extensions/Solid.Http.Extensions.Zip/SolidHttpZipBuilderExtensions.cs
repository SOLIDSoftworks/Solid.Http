using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using Solid.Http.Extenstions.Zip;
using Solid.Http.Extenstions.Zip.Abstraction;
using Solid.Http.Extenstions.Zip.Providers;
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
        /// <para>Can create a deserializer for application/zip</para>
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="additionalMimeTypes">Additional zip mime types</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder AddZip(this ISolidHttpBuilder builder, params string[] additionalMimeTypes) =>
            builder.AddZip(ZipArchiveMode.Read, additionalMimeTypes);

        /// <summary>
        /// Adds support for ZipArchive
        /// <para>Can create a deserializer for application/zip</para>
        /// </summary>
        /// <param name="builder">The setup</param>
        /// <param name="additionalMimeTypes">Additional zip mime types</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpCoreBuilder AddZip(this ISolidHttpCoreBuilder builder, params string[] additionalMimeTypes) =>
            builder.AddZip(ZipArchiveMode.Read, additionalMimeTypes);

        /// <summary>
        /// Adds support for ZipArchive
        /// <para>Can create a deserializer for application/zip</para>
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="mode">The zip archive mode</param>
        /// <param name="additionalMimeTypes">Additional zip mime types</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder AddZip(this ISolidHttpBuilder builder, ZipArchiveMode mode, params string[] additionalMimeTypes)
        {
            var provider = new ZipArchiveSerializerSettingsProvider(mode);
            builder.Services.AddSingleton<IZipSerializerSettingsProvider>(provider);
            builder.AddDeserializer<ZipArchiveResponseDeserializerFactory>("application/zip");
            return builder;
        }

        /// <summary>
        /// Adds support for ZipArchive
        /// <para>Can create a deserializer for application/zip</para>
        /// </summary>
        /// <param name="builder">The setup</param>
        /// <param name="mode">The zip archive mode</param>
        /// <param name="additionalMimeTypes">Additional zip mime types</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpCoreBuilder AddZip(this ISolidHttpCoreBuilder builder, ZipArchiveMode mode, params string[] additionalMimeTypes)
            => ((ISolidHttpBuilder)builder).AddZip(mode, additionalMimeTypes) as ISolidHttpCoreBuilder;
    }
}
