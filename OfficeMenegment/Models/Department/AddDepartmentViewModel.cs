namespace OfficeMenegment.Models.Department
{
    public class AddDepartmentViewModel
    {
        public string Name { get; set; }

        public ICollection<EmployeeDbModel> Employees { get; set; }
    }
}
