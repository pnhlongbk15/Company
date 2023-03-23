namespace Business.Models
{
    public class DepartmentModel
    {
        public string Name { get; set; }
        public List<EmployeeModel> Employees { get; set; }
    }
}
