namespace Pharmacy_Management_System.DependencyExtensions
{
    public static class RegisterCorsPolicy
    {
        public static void AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
#if !DEBUG
            var allowedOrigins = configuration.GetSection("AllowedOrigin").Value?.Split(';', StringSplitOptions.RemoveEmptyEntries);

            if (allowedOrigins == null || allowedOrigins.Length == 0)
            {
                throw new ArgumentException("No valid origins specified in AllowedOrigin configuration.");
            }
#endif

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy", policy =>
                {
#if !DEBUG
                    policy.WithOrigins(allowedOrigins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("x-pagination")
                        .WithExposedHeaders("x-updatetime");
#else
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .WithExposedHeaders("x-pagination")
                          .WithExposedHeaders("x-updatetime");
#endif
                });
            });
        }
    }
}
