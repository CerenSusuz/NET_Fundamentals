using System.Text.Json.Serialization;

namespace JsonSerialization;

public class Department
{
    [JsonPropertyName("departmentName")]
    public string DepartmentName { get; set; }

    [JsonPropertyName("employees")]
    public List<Employee> Employees { get; set; }
}
