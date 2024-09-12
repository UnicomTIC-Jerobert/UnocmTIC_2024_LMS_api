using Microsoft.Data.Sqlite;

namespace LMS.rest_api.Data
{
    public class DatabaseInitializer
    {
        private readonly string _connectionString;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Initialize()
        {
            Console.WriteLine("Running DatabaseInitializer Initialize Fuction...");
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Employee (
                        EmpId INTEGER PRIMARY KEY,
                        FirstName TEXT NOT NULL,
                        LastName TEXT NOT NULL,
                        Dob TEXT NOT NULL
                    );

                    -- Insert sample data if tables are empty
                    INSERT OR IGNORE INTO Employee (EmpId, FirstName, LastName, Dob) VALUES
                    (1, 'John', 'Doe', '1980-01-01'),
                    (2, 'Jane', 'Smith', '1990-05-15'),
                    (3, 'Sam', 'Brown', '1985-08-10');

                             
                ";
                command.ExecuteNonQuery();
            }
        }
    }
}
