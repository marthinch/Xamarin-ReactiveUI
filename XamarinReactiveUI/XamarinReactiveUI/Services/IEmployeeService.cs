using System.Collections.Generic;
using System.Linq;
using XamarinReactiveUI.Models;

namespace XamarinReactiveUI.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(int id);
        void AddEmployee(Employee employee);
        void EditEmployee(int id, Employee employee);
        void DeleteEmployee(int id);
    }

    public class EmployeeService : IEmployeeService
    {
        private static List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Andi", Phone = "0000000", Address = "block a1" },
            new Employee { Id = 2, Name = "Aldi", Phone = "1111111", Address = "block b1" },
            new Employee { Id = 3, Name = "Butet", Phone = "22222222", Address = "block c1" },
            new Employee { Id = 4, Name = "Jangoal", Phone = "3333333", Address = "block d1" },
            new Employee { Id = 5, Name = "Udin", Phone = "4444444", Address = "block e1" }
        };

        public IEnumerable<Employee> GetEmployees()
        {
            return employees;
        }

        public Employee GetEmployee(int id)
        {
            return employees.SingleOrDefault(a => a.Id == id);
        }

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        public void EditEmployee(int id, Employee employee)
        {
            DeleteEmployee(id);

            employees.Add(employee);
        }

        public void DeleteEmployee(int id)
        {
            var currentEmployee = GetEmployee(id);
            if (currentEmployee == null)
                return;

            employees.Remove(currentEmployee);
        }
    }
}
