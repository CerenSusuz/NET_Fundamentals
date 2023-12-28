using System.Text.Json;

namespace DeepCloningApp
{
    public static class SerializationHelper
    {
        public static T DeepCopyWithJson<T>(T obj)
        {
            var jsonString = JsonSerializer.Serialize(value: obj);
            
            return JsonSerializer.Deserialize<T>(json: jsonString);
        }
    }
}
