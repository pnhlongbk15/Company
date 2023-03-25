namespace Business.Models
{
    public class EmployeeModel
    {
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //public DepartmentModel? Department { get; set; }

        // No mapping
        public string DepartmentName { get; set; }
    }
}
