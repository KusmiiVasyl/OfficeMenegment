using System.ComponentModel.DataAnnotations;


namespace OfficeMenegment.Models
{
    public class DepartmentDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EmployeeDbModel> Employees { get; set; }
    }
}

