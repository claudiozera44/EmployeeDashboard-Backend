using EmployeeDashboard.Api.Models;
using System.Text.Json;

namespace EmployeeDashboard.Api.Services;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(HttpClient httpClient, ILogger<EmployeeService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://randomuser.me/api/?results=50");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<RandomUserApiResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (apiResponse?.Results == null)
            {
                _logger.LogWarning("No results returned from RandomUser API");
                return new List<Employee>();
            }

            return apiResponse.Results.Select(TransformToEmployee).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching employees from RandomUser API");
            throw;
        }
    }

    private static Employee TransformToEmployee(RandomUserResult result)
    {
        string postalCode = result.Location.Postcode.ValueKind switch
        {
            JsonValueKind.String => result.Location.Postcode.GetString() ?? string.Empty,
            JsonValueKind.Number => result.Location.Postcode.GetInt32().ToString(),
            _ => string.Empty
        };

        return new Employee
        {
            Id = result.Login.Uuid,
            FirstName = result.Name.First,
            LastName = result.Name.Last,
            Email = result.Email,
            Phone = result.Phone,
            PictureUrl = result.Picture.Large,
            Address = new Address
            {
                Street = $"{result.Location.Street.Number} {result.Location.Street.Name}",
                City = result.Location.City,
                State = result.Location.State,
                Country = result.Location.Country,
                PostalCode = postalCode
            }
        };
    }
}
