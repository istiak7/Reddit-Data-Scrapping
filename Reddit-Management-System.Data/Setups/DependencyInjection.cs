using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reddit_Management_System.Data.DbContexts;
using Reddit_Management_System.Domain.Interfaces.Transactions;
using Reddit_Management_System.Domain.Contexts;

namespace Reddit_Management_System.Data.Setups
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            string? connectionString)
        {
            #region Null Checks

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            #endregion

            #region Register Raw Connection

            services.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(connectionString));

            #endregion


            #region Register DbContext

            services.AddDbContext<ApplicationDbContextWrite>((provider, options) =>
            {
                options.UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContextWrite).Assembly.FullName));

#if DEBUG
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
#endif
            }, ServiceLifetime.Scoped);

            services.AddScoped<IApplicationDbContext>
                (provider => provider.GetRequiredService<ApplicationDbContextWrite>());

            #endregion

            services.AddScoped<ITransactionUtil, TransactionUtil>();
            
            return services;
        }
    }
}   
