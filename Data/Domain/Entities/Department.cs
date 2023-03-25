using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class Department
    {
        [Key]
        [StringLength(255)]
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        private List<Employee> employees = new List<Employee>();
        public List<Employee> Employees { get { return employees; } }
    }
}
