using Asp.Versioning;

namespace WaveChat.Authorization.Configuration
{
    public static class VersioningConfiguration
    {
        // <summary>
        /// Add version support for API
        /// 
        /// <param name="services">Services collection</param>
        public static IServiceCollection AddAppVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;

                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            })
                .AddMvc()
                .AddApiExplorer(options =>
                {
                    options.SubstituteApiVersionInUrl = true;
                    options.GroupNameFormat = "'v'VVV";
                    options.FormatGroupName = (group, version) => $"{group} - {version}";
                });

            return services;
        }
    }
}
