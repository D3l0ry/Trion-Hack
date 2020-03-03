using System;
using System.Reflection;
using System.Text;

namespace Trion.SDK.Serializers
{
    internal class TrionObjectSerializer : ISerializer
    {
        public string Serialize(object type)
        {
            StringBuilder Out = new StringBuilder();

            foreach (var Obj in type.GetType().GetProperties())
            {
                Out.Append($"{Obj.Name}={Obj.GetValue(type, null)}\n");
            }

            return Out.ToString();
        }

        public object Deserialize(string input, object type)
        {
            foreach (var Arg in input.Split('\n'))
            {
                var Args = Arg.Split('=');

                type.GetType().GetRuntimeProperty(Args[0])?.SetValue(type, Convert.ChangeType(Args[1].TrimEnd(), type.GetType().GetRuntimeProperty(Args[0]).PropertyType));
            }

            return type;
        }

        public T Deserialize<T>(string input) => (T)Deserialize(input, (T)(default));
    }
}