using Microsoft.Data.SqlClient;
using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Services;

public class EmployeeRepository: IEmployeeRepository
{
    private readonly string _connectionString;

    public EmployeeRepository(string connectionString)
    {
        _connectionString = connectionString;

    }

    public bool AddEmployee(Employee employee)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        const string checkIfExistsQuery = "SELECT DepId FROM Department WHERE DepID = @DepID";
        using var checkCommand = new SqlCommand(checkIfExistsQuery, connection);
        checkCommand.Parameters.AddWithValue("@DepID", employee.DepId);

        var existingDep = checkCommand.ExecuteScalar();

        if (existingDep == null)
        {
            throw new Exception($"Department with the id {employee.DepId} does not exist.");
        }

        if (employee.ManagerId != null)
        {
            const string checkIfExistsQuery2 = "SELECT EmpID FROM Employees WHERE EmpID = @EmpID";
            using var checkCommand2 = new SqlCommand(checkIfExistsQuery2, connection);
            checkCommand2.Parameters.AddWithValue("@EmpID", employee.ManagerId);

            var existingManager = checkCommand2.ExecuteScalar();

            if (existingManager == null)
            {
                throw new Exception($"Manager with the id {employee.ManagerId} does not exist.");
            }
        }

        const string insertString = "INSERT INTO Employee(EmpName, JobName, ManagerId, HireDate, Salary, Commission, DepId) VALUES (@EmpName, @JobName, @ManagerId, @HireDate, @Salary, @Commission, @DepId)";
        var countRowsAdded = -1;
        
        SqlCommand command = new(insertString, connection);
        command.Parameters.AddWithValue("EmpName", employee.EmpName);
        command.Parameters.AddWithValue("JobName", employee.JobName);
        
        int? manager = employee.ManagerId;
        if (manager == null)
        {
            command.Parameters.AddWithValue("ManagerId", DBNull.Value);
        }
        else
        {
            command.Parameters.AddWithValue("ManagerId", manager);
        }

        var date = DateTime.Now.Date;
        
        command.Parameters.AddWithValue("HireDate", date); 
        command.Parameters.AddWithValue("Salary", employee.Salary);
        command.Parameters.AddWithValue("Commission", employee.Commission);
        command.Parameters.AddWithValue("DepId", employee.DepId);
        
        countRowsAdded = command.ExecuteNonQuery();
       

        return countRowsAdded != -1;
    }
    
    
    public bool UpdateEmployee(int id, AddEmployeeDto employee)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        const string checkIfExistsQuery = "SELECT DepId FROM Department WHERE DepID = @DepID";
        using var checkCommand = new SqlCommand(checkIfExistsQuery, connection);
        checkCommand.Parameters.AddWithValue("@DepID", employee.DepId);

        var existingDep = checkCommand.ExecuteScalar();

        if (existingDep == null)
        {
            throw new Exception($"Department with the id {employee.DepId} does not exist.");
        }
        
        if (employee.ManagerId != null)
        {
            const string checkIfExistsQuery2 = "SELECT EmpID FROM Employees WHERE EmpID = @EmpID";
            using var checkCommand2 = new SqlCommand(checkIfExistsQuery2, connection);
            checkCommand2.Parameters.AddWithValue("@EmpID", employee.ManagerId);

            var existingManager = checkCommand2.ExecuteScalar();

            if (existingManager == null)
            {
                throw new Exception($"Manager with the id {employee.ManagerId} does not exist.");
            }
        }
        
        const string query = "UPDATE Employees SET EmpName = @EmpName, JobName = @JobName, ManagerID = @ManagerID, HireDate = @HireDate, Salary = @Salary, Commission = @Commission, DepID = @DepID WHERE EmpID = @EmpID";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@EmpID", id);
        command.Parameters.AddWithValue("@EmpName", employee.EmpName);
        command.Parameters.AddWithValue("@JobName", employee.JobName);
        
        int? manager = employee.ManagerId;
        if (manager == null)
        {
            command.Parameters.AddWithValue("ManagerId", DBNull.Value);
        }
        else
        {
            command.Parameters.AddWithValue("ManagerId", manager);
        }
        
        command.Parameters.AddWithValue("@HireDate", employee.HireDate);
        command.Parameters.AddWithValue("@Salary", employee.Salary);
        command.Parameters.AddWithValue("@Commission", employee.Commission);
        command.Parameters.AddWithValue("@DepId", employee.DepId);

        try
        {
            var countRows = command.ExecuteNonQuery();
            return countRows > 0;
        }
        catch (Exception e)
        {
            throw new Exception("Error updating employee.", e);
        }
    }

    public bool DeleteEmployee(int id)
    {
        const string query = "DELETE FROM Employees WHERE EmpID = @EmpID";
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand(query, connection);
        
        command.Parameters.AddWithValue("@EmpID", id);

        try
        {
            connection.Open();
            var countRows = command.ExecuteNonQuery();
            return countRows > 0;
        }
        catch (Exception e)
        {
            throw new Exception("Error deleting employee.", e);
        }
    }
}