namespace Trion.SDK.Serializers
{
    internal interface ISerializer
    {
        string Serialize(object Type);

        object Deserialize(string Input, object Type);

        T Deserialize<T>(string Input);
    }
}