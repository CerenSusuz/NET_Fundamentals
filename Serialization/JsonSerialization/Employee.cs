using System.Text.Json.Serialization;

namespace JsonSerialization;

public class Employee
{
    [JsonPropertyName("employeeName")]
    public string EmployeeName { get; set; }
}
