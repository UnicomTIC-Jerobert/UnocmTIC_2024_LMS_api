using LMS.rest_api.Models;
using Microsoft.Data.Sqlite;

namespace LMS.rest_api.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = new List<Employee>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT EmpId, FirstName, LastName, Dob FROM Employee";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmpId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Dob = reader.GetDateTime(3)
                        });
                    }
                }
            }
            return employees;
        }

        public Employee GetById(int empId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT EmpId, FirstName, LastName, Dob FROM Employee WHERE EmpId = @empId";
                command.Parameters.AddWithValue("@empId", empId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Employee
                        {
                            EmpId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Dob = reader.GetDateTime(3)
                        };
                    }
                }
            }
            return null;
        }

        public void Add(Employee employee)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Employee (FirstName, LastName, Dob) VALUES (@firstName, @lastName, @dob)";
                command.Parameters.AddWithValue("@firstName", employee.FirstName);
                command.Parameters.AddWithValue("@lastName", employee.LastName);
                command.Parameters.AddWithValue("@dob", employee.Dob);
                command.ExecuteNonQuery();
            }
        }

        public void Update(Employee employee)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Employee SET FirstName = @firstName, LastName = @lastName, Dob = @dob WHERE EmpId = @empId";
                command.Parameters.AddWithValue("@empId", employee.EmpId);
                command.Parameters.AddWithValue("@firstName", employee.FirstName);
                command.Parameters.AddWithValue("@lastName", employee.LastName);
                command.Parameters.AddWithValue("@dob", employee.Dob);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int empId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Employee WHERE EmpId = @empId";
                command.Parameters.AddWithValue("@empId", empId);
                command.ExecuteNonQuery();
            }
        }
    }
}
