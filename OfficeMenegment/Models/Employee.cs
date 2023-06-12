namespace OfficeMenegment.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }
        public DateTime DateBirthday { get; set; }
    }
}
