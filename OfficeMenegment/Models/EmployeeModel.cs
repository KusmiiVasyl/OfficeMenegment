namespace OfficeMenegment.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }
        public DateTime DateBirthday { get; set; }
    }
}
