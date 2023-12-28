namespace DeepCloningApp
{
    [Serializable]
    public class Department
    {
        public string DepartmentName { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
