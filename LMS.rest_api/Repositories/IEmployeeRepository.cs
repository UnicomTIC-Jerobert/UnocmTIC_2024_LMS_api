using LMS.rest_api.Models;

namespace LMS.rest_api.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int empId);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(int empId);
    }
}
