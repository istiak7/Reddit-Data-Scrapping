namespace Pharmacy_Management_System.DependencyExtensions
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

                options.AddPolicy("PharmacistOnly", policy =>
                {
                    policy.RequireRole("PHARMACIST");
                });

                options.AddPolicy("StaffAndAbove", policy =>
                {
                    policy.RequireRole("STAFF", "PHARMACIST", "ADMIN");
                });

                options.AddPolicy("AllAuthenticated", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });
        }
    }
}
