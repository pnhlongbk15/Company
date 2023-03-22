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
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(255)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }
    }
}
