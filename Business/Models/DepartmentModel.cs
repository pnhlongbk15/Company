namespace Business.Models
{
    public class DepartmentModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }

        private List<EmployeeModel> employees = new List<EmployeeModel>();
        public List<EmployeeModel> Employees { get { return employees; } }
    }
}
