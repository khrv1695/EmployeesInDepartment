using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    class DataZone
    {
        static void Main(string[] args)
        {
            getDepartmentEmployees();
        }

        private static void getDepartmentEmployees()
        {
            HRContext hrDb = new HRContext();
            var departmentEmployees = hrDb.Departments.GroupJoin(hrDb.Employees, d => d.Id, e => e.DepartmentId,
                   (d, emp) => new
                   {
                       departments = d,
                       employes = emp.OrderByDescending(e => e.Name).ToList()
                   }
               ).OrderBy(d => d.departments.Name).ToList();
        }
    }

    public class HRContext : DbContext
    {
        public HRContext() : base("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=HR;Integrated Security=True")
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
