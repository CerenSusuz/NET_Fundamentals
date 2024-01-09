using DeepCloningApp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Creating department...");
        var department = new Department()
        {
            DepartmentName = "HR",
            Employees = new List<Employee>() { new Employee() { EmployeeName = "Ceren" } }
        };

        Console.WriteLine("Cloning department...");
        var clonedDepartment = SerializationHelper.DeepCopyWithJson(department);

        Console.WriteLine("Modifying clone...");
        clonedDepartment.DepartmentName = "Management";
        clonedDepartment.Employees.First().EmployeeName = "Chucky";

        Console.WriteLine("Printing results...");
        Console.WriteLine($"{department.DepartmentName} {department.Employees.First().EmployeeName}");
        Console.WriteLine($"{clonedDepartment.DepartmentName} {clonedDepartment.Employees.First().EmployeeName}");

        Console.ReadKey();
    }
}