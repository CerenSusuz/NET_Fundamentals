using BinarySerialization;
using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    static void Main(string[] args)
    {
        var department = new Department()
        {
            DepartmentName = "Development",
            Employees = new List<Employee>() { new Employee() { EmployeeName = "Ceren" } }
        };

        var formatter = new BinaryFormatter();
        
        using (Stream stream = new FileStream(path: "BinaryData.txt", mode: FileMode.Create, access: FileAccess.Write, share: FileShare.None))
        {
            formatter.Serialize(serializationStream: stream, graph: department);
        }

        Department? deserialized;
        
        using (Stream stream = new FileStream(path: "BinaryData.txt", mode: FileMode.Open, access: FileAccess.Read, share: FileShare.Read))
        {
            deserialized = (Department)formatter.Deserialize(serializationStream: stream);
        }

        Console.WriteLine($"Deserialized department: {deserialized.DepartmentName}");
    }
}