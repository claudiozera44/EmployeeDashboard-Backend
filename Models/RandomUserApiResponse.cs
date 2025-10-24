using System.Text.Json;

namespace EmployeeDashboard.Api.Models;

public class RandomUserApiResponse
{
    public List<RandomUserResult> Results { get; set; } = new();
}

public class RandomUserResult
{
    public RandomUserName Name { get; set; } = new();
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public RandomUserPicture Picture { get; set; } = new();
    public RandomUserLocation Location { get; set; } = new();
    public RandomUserLogin Login { get; set; } = new();
}

public class RandomUserName
{
    public string First { get; set; } = string.Empty;
    public string Last { get; set; } = string.Empty;
}

public class RandomUserPicture
{
    public string Large { get; set; } = string.Empty;
    public string Medium { get; set; } = string.Empty;
    public string Thumbnail { get; set; } = string.Empty;
}

public class RandomUserLocation
{
    public RandomUserStreet Street { get; set; } = new();
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public JsonElement Postcode { get; set; }
}

public class RandomUserStreet
{
    public int Number { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class RandomUserLogin
{
    public string Uuid { get; set; } = string.Empty;
}
