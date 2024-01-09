using System.Runtime.Serialization;

namespace CustomBinarySerializer
{
    [Serializable]
    public class Person : ISerializable
    {
        public int PersonId { get; set; }

        public string FullName { get; set; }

        public Person(int personId, string fullName)
        {
            PersonId = personId;
            FullName = fullName;
        }

        protected Person(SerializationInfo info, StreamingContext context)
        {
            PersonId = info.GetInt32("PersonId");
            FullName = info.GetString("FullName");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("PersonId", PersonId);
            info.AddValue("FullName", FullName);
        }
    }
}
