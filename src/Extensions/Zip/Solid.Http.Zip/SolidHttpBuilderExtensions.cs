using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.Zip;
using System;
using System.IO.Compression;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for adding <see cref="ZipArchive" /> deserialization to <see cref="SolidHttpBuilder" />.
    /// </summary>
    public static class Solid_Http_Zip_SolidHttpBuilderExtensions
    {
        /// <summary>
        /// Adds <see cref="ZipArchive" /> deserialization to <see cref="SolidHttpBuilder" />.
        /// <para>This ZIP deserialization uses System.IO.Compression.</para>
        /// </summary>
        /// <param name="builder">The <see cref="SolidHttpBuilder" /> that is being extended.</param>
        /// <returns>The <see cref="SolidHttpBuilder" /> so that additional calls can be chained.</returns>
        public static SolidHttpBuilder AddZip(this SolidHttpBuilder builder)
        {
            builder.Services.TryAddSingleton<ZipArchiveDeserializer>();
            builder.AddDeserializer(p => p.GetService<ZipArchiveDeserializer>());
            return builder;
        }

        /// <summary>
        /// Adds <see cref="ZipArchive" /> deserialization to <see cref="SolidHttpBuilder" />.
        /// <para>This ZIP deserialization uses System.IO.Compression.</para>
        /// </summary>
        /// <param name="builder">The <see cref="SolidHttpBuilder" /> that is being extended.</param>
        /// <param name="configureOptions">A delegate used to configure <see cref="SolidHttpZipOptions" />.</param>
        /// <returns>The <see cref="SolidHttpBuilder" /> so that additional calls can be chained.</returns>
        public static SolidHttpBuilder AddZip(this SolidHttpBuilder builder, Action<SolidHttpZipOptions> configureOptions)
        {
            builder.AddZip();
            builder.Services.ConfigureSolidHttpZip(configureOptions);
            return builder;
        }
    }
}
