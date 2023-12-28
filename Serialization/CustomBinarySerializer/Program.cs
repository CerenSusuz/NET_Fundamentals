using CustomBinarySerializer;
using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    static void Main(string[] args)
    {
        var person = new Person(1, "Ceren Susuz");
        Console.WriteLine($"Created PersonId: {person.PersonId}, FullName: {person.FullName}");

        var formatter = new BinaryFormatter();

        try
        {
            using (Stream stream = new FileStream("PersonData.bin", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, person);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to serialize. Reason: " + e.Message);
            throw;
        }

        Person deserializedPerson;

        try
        {
            using (Stream stream = new FileStream("PersonData.bin", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                deserializedPerson = (Person)formatter.Deserialize(serializationStream: stream);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
            throw;
        }

        Console.WriteLine($"Deserialized PersonId: {deserializedPerson.PersonId}, FullName: {deserializedPerson.FullName}");
        Console.ReadKey();
    }
}