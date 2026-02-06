using Pharmacy_Management_System.Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Domain.Entities.Users
{
    [Table("Users", Schema = "public")]
    public class User : BaseEntity
    {
        [Required]
        [Column("username"), MaxLength(128)]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        [Column("email"), MaxLength(128)]
        public required string Email { get; set; }

        [Required]
        [Column("password"), MaxLength(256)]
        public required string Password { get; set; }

        [Column("refresh_token"), MaxLength(512)]
        public string? RefreshToken { get; set; }

        [Column("refresh_token_expire_time"), MaxLength(512)]
        public DateTime? RefreshTokenExpireTime { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId { get; set; } //FK
        public Role Role { get; set; }


    }
}
