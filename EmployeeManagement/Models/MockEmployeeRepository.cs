namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepository() 
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Rojan", Email = "rojanshakya071@gmail.com", Department = Dept.abc},
                new Employee() { Id = 2, Name = "Palisha", Email = "palishashakya37@gmail.com", Department = Dept.xyz},
                new Employee() {Id = 3, Name = "Junu", Email = "junushakya17@gmail.com", Department = Dept.mno}
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(x => x.Id)+1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == id);
            if(employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int? Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }

        public Employee Update(Employee EmployeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == EmployeeChanges.Id);
            if (employee != null)
            {
                employee.Name = EmployeeChanges.Name;
                employee.Email = EmployeeChanges.Email;
                employee.Department = EmployeeChanges.Department;
            }
            return employee;
        }
    }
}
