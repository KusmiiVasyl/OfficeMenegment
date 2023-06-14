namespace OfficeMenegment.Models
{
    public class UpdateEmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
        public ICollection<DepartmentDbModel> Departments { get; set; }
        public DateTime DateBirthday { get; set; }
    }
}
