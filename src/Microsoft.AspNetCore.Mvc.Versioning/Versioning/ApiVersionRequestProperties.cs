﻿namespace Microsoft.AspNetCore.Mvc.Versioning
{
    using Extensions.DependencyInjection;
    using Http;
    using System;

    /// <content>
    /// Provides additional implementation specific to ASP.NET Core.
    /// </content>
    public partial class ApiVersionRequestProperties
    {
        readonly HttpContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiVersionRequestProperties"/> class.
        /// </summary>
        /// <param name="context">The current <see cref="HttpContext">HTTP context</see>.</param>
        [CLSCompliant( false )]
        public ApiVersionRequestProperties( HttpContext context )
        {
            Arg.NotNull( context, nameof( context ) );

            this.context = context;
            rawApiVersion = new Lazy<string>( GetRawApiVersion );
        }

        string GetRawApiVersion()
        {
            var reader = context.RequestServices.GetService<IApiVersionReader>() ?? new QueryStringApiVersionReader();
            return reader.Read( context.Request );
        }
    }
}