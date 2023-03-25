using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class Employee
    {
        [Key]
        [StringLength(255)]
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(255)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string DepartmentId { get; set; }
        /*
        [Required]
        [Column("DepartmentId")]
        public Department Department { get; set; }

        /*
        [NotMapped]
        [StringLength(255)]
        public string? DepartmentName { get; set; }
        */
    }
}
