using EmployeeDashboard.Api.Models;

namespace EmployeeDashboard.Api.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetEmployeesAsync();
}
