using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Rojan",
                    Department = Dept.xyz,
                    Email = "rojanshakya071@gmail.com"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Palisha",
                    Department = Dept.mno,
                    Email = "palishashakya39@gmail.com"
                }
            );
        }
    }
}
