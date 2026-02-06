using Reddit_Management_System.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reddit_Management_System.Domain.Entities
{
    public partial class BaseEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } 
        [Required]
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; } 
        public int? UpdatedBy { get; set; }
        [Required]
        public int IsActive { get; set; } = 1;

        public void Active()
        {
            IsActive = (int)EntityConstant.StatusId.Active;
        }

        public void InActive()
        {
            IsActive = (int)EntityConstant.StatusId.InActive;
        }

        public void Delete()
        {
            IsActive = (int)EntityConstant.StatusId.Delete;
        }
        public void SetDefaultValueDuringInsert(DateTime createdAt)
        {
            CreatedAt = createdAt;
            IsActive = (int)EntityConstant.StatusId.Active;
        }
        public void SetDefaultValueDuringUpdate(DateTime updatedAt)
        {
            UpdatedAt = updatedAt;
        }
    }
}
