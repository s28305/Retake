using WebApplication2.Models;
using Microsoft.Data.SqlClient;
using WebApplication2.DTOs;

namespace WebApplication2.Services;

public class DepartmentRepository: IDepartmentRepository
{
    private readonly string _connectionString;

    public DepartmentRepository(string connectionString)
    {
        _connectionString = connectionString;

    }
    
    public async Task<IEnumerable<Department>> GetAll()
    {
        List<Department> departments = new();
        const string queryString = "SELECT * FROM Department";
        await using SqlConnection connection = new(_connectionString);
        SqlCommand command = new(queryString, connection);
        connection.Open();
        var reader = await command.ExecuteReaderAsync();
        try
        {
            if (reader.HasRows)
            {
                while (reader.Read()) 
                {
                    var department = new Department {
                        DepName = reader.GetString(1),
                        DepLocation = reader.GetString(1)
                    };
                    departments.Add(department);
                }
            }
        }
        finally 
        {
            reader.Close();
        }

        return departments;
    }
    
    public Department? GetById(int id)
    {
        Department? department = null;
        const string queryString = "SELECT * FROM Department WHERE DepID = @depId";
        using SqlConnection connection = new(_connectionString);
        SqlCommand command = new(queryString, connection);
        command.Parameters.AddWithValue("depId", id);
        connection.Open();
        var reader = command.ExecuteReader();
        try
        {
            if (reader.HasRows)
            {
                while (reader.Read()) 
                {
                    department = new Department {
                        DepName = reader.GetString(1),
                        DepLocation = reader.GetString(1)
                    };
                } 
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally {
            reader.Close();
        }

        return department;
    }

    public bool AddDepartment(Department department)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string checkIfExistsQuery = "SELECT DepName FROM Department WHERE DepName = @DepName";
        using (var checkCommand = new SqlCommand(checkIfExistsQuery, connection))
        {
            checkCommand.Parameters.AddWithValue("@DepName", department.DepName);
            var existingDep = checkCommand.ExecuteScalar();

            if (existingDep != null)
            {
                throw new Exception($"Department with the name {department.DepName} exists.");
            }
        }
    
        const string insertString = "INSERT INTO Department(DepName, DepLocation) VALUES (@DepName, @DepLocation)";
        using (var command = new SqlCommand(insertString, connection))
        {
            command.Parameters.AddWithValue("@DepName", department.DepName);
            command.Parameters.AddWithValue("@DepLocation", department.DepLocation);

            var countRowsAdded = command.ExecuteNonQuery();
            return countRowsAdded > 0;
        }
    }
}