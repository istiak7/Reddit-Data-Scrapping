using Microsoft.EntityFrameworkCore;
using Pharmacy_Management_System.Application.Common.Utilities;
using Pharmacy_Management_System.Data.DbContexts.ModelBuilders;
using Pharmacy_Management_System.Domain.Contexts;
using Pharmacy_Management_System.Domain.Entities;
using Pharmacy_Management_System.Domain.Entities.Permissions;
using Pharmacy_Management_System.Domain.Entities.Roles;
using Pharmacy_Management_System.Domain.Entities.Users;
using System.Reflection;

namespace Pharmacy_Management_System.Data.DbContexts
{
    public partial class ApplicationDbContextWrite : DbContext, IApplicationDbContext
    {
        #region Constructor
        public ApplicationDbContextWrite()
        {

        }

        public ApplicationDbContextWrite(DbContextOptions<ApplicationDbContextWrite> options) : base(options)
        {

        }
        #endregion Constructor

        #region DbSets

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ConfigureAllModelBuilders();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        #region SaveChanges

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            int result = 0;
            foreach (var entry in ChangeTracker.Entries<BaseEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.SetDefaultValueDuringInsert(CommonMethods.GetBDCurrentTime());
                        break;

                    case EntityState.Modified:
                        entry.Entity.SetDefaultValueDuringUpdate(CommonMethods.GetBDCurrentTime());
                        break;
                }
            }
            result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        #endregion
    }
}
