using JsonSerialization;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };

        var department = new Department()
        {
            DepartmentName = "Development",
            Employees = [new Employee() { EmployeeName = "Ceren" }]
        };

        var jsonString = JsonSerializer.Serialize(value: department, options: options);
        File.WriteAllText(path: @"./department.json", contents: jsonString);

        var departmentJSON = File.ReadAllText(@"./department.json");
        var deserialized = JsonSerializer.Deserialize<Department>(json: departmentJSON, options: options);
        Console.WriteLine($"Deserialized department: { deserialized.DepartmentName}");
    }
}