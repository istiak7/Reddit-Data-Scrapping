using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reddit_Management_System.Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Data.Configurations.Roles
{
    public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions", "public");

            //primary key
            builder.HasKey(x => x.Id);

            builder.HasIndex(rp => new { rp.RoleId, rp.PermissionId })
                    .IsUnique();

            builder.HasOne(r => r.Role)
                .WithMany(rp => rp.RolePermissions)
                .HasForeignKey(r => r.RoleId);

            builder.HasOne(r =>  r.Permission)
                .WithMany(rp => rp.RolePermissions)
                .HasForeignKey(r => r.PermissionId);
        }
    }
}
