namespace EmployeeManagement.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        public readonly AppDbContext context;
        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = context.Employees.Find(id);
            if(employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int? Id)
        {
            return context.Employees.FirstOrDefault(x=>x.Id == Id);
        }

        public Employee Update(Employee EmployeeChanges)
        {
            var employee = context.Employees.Find(EmployeeChanges.Id);
            if(employee != null)
            {
                employee.Name = EmployeeChanges.Name;
                employee.Email = EmployeeChanges.Email;
                employee.Department = EmployeeChanges.Department;
                employee.Photo = EmployeeChanges.Photo;
                context.SaveChanges();
            }
            return employee;
        }
    }
}
