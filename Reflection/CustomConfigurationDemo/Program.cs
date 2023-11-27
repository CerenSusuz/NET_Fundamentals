using CustomConfigurationDemo;

var myConfigurationComponent = new ConfigurationComponent();

Console.WriteLine("Loading settings...");
myConfigurationComponent.LoadSettings();

int myIntValue = myConfigurationComponent.IntProperty;
string myStringValue = myConfigurationComponent.StringProperty;
Console.WriteLine($"IntProperty: {myIntValue}");
Console.WriteLine($"StringProperty: {myStringValue}");

myConfigurationComponent.IntProperty = 20031997;
myConfigurationComponent.StringProperty = "Hello, World! -gerund";
Console.WriteLine("Updating property values...");
Console.WriteLine($"IntProperty: {myConfigurationComponent.IntProperty}");
Console.WriteLine($"StringProperty: {myConfigurationComponent.StringProperty}");

Console.WriteLine("Saving settings...");
myConfigurationComponent.SaveSettings();

Console.WriteLine("Done!");

Console.ReadKey();