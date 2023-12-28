using System.Xml.Serialization;

namespace XMLSerialization;

[XmlRoot("Employee")]
public class Employee
{
    [XmlElement("EmployeeName")]
    public string EmployeeName { get; set; }
}
