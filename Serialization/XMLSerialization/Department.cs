using System.Xml.Serialization;

namespace XMLSerialization;

[XmlRoot("Department")]
public class Department
{
    [XmlElement("DepartmentName")]
    public string DepartmentName { get; set; }

    [XmlElement("Employees")]
    public List<Employee> Employees { get; set; }
}
