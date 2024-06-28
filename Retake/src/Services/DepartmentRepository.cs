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
    
    public async Task<IEnumerable<GetDeptDto>> GetAll()
    {
        List<GetDeptDto> departments = new();
        const string queryString = "SELECT * FROM Department";
        const string employeeQuery = "SELECT * FROM Employees WHERE DepID = @DepID";
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
                    var department = new GetDeptDto {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Location = reader.GetString(2)
                    };
                    departments.Add(department);
                }
            }
        }
        finally 
        {
            reader.Close();
        }
        
        foreach (var department in departments)
        {
            var employeeCommand = new SqlCommand(employeeQuery, connection);
            employeeCommand.Parameters.AddWithValue("@DepID", department.Id);
        
            var employeeReader = await employeeCommand.ExecuteReaderAsync();
        
            try
            {
                while (await employeeReader.ReadAsync())
                {
                    var employee = new Employee
                    {
                        EmpName = employeeReader.GetString(1),
                        JobName = employeeReader.GetString(2),
                        ManagerId = employeeReader.IsDBNull(3) ? (int?)null : employeeReader.GetInt32(3),
                        Salary = employeeReader.GetDecimal(5),
                        Commission = employeeReader.IsDBNull(6) ? (decimal?)null : employeeReader.GetDecimal(6),
                        DepId = employeeReader.GetInt32(7)
                    };
                
                    department.Employees.Add(employee);
                }
            }
            finally
            {
                await employeeReader.CloseAsync();
            }
        }
        
        

        return departments;
    }
    
    public GetOneDeptDto? GetById(int id)
    {
        GetOneDeptDto? department = null;
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
                    department = new GetOneDeptDto {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Location = reader.GetString(2)
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