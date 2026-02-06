namespace Reddit_Management_System.DependencyExtensions
{
    public static class RegisterAuthPolicy
    {
        public static void AddAuthPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireRole("ADMIN");
                });

                options.AddPolicy("ModeratorOnly", policy =>
                {
                    policy.RequireRole("MODERATOR");
                });

                options.AddPolicy("StaffAndAbove", policy =>
                {
                    policy.RequireRole("STAFF", "MODERATOR", "ADMIN");
                });

                options.AddPolicy("AllAuthenticated", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });
        }
    }
}
