using System.Xml.Serialization;
using XMLSerialization;

class Program
{
    static void Main(string[] args)
    {
        Department department = new()
        {
            DepartmentName = "Development",
            Employees = [new Employee { EmployeeName = "Ceren" }]
        };

        var serializer = new XmlSerializer(typeof(Department));

        using (TextWriter writer = new StreamWriter(@"./department.xml"))
        {
            serializer.Serialize(writer, department);
        }

        Department? deserialized;

        using (TextReader reader = new StreamReader(@"./department.xml"))
        {
            deserialized = serializer.Deserialize(textReader: reader) as Department;
        }

        Console.WriteLine($"Deserialized department: {deserialized.DepartmentName}");
    }
}