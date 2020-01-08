using System;
using System.Reflection;
using System.Text;

namespace Trion.SDK.Serializers
{
    internal class TrionObjectSerializer : ISerializer
    {
        public string Serialize(object Type)
        {
            StringBuilder Out = new StringBuilder();

            foreach (var Obj in Type.GetType().GetProperties())
            {
                Out.Append($"{Obj.Name}={Obj.GetValue(Type, null)}\n");
            }

            return Out.ToString();
        }

        public object Deserialize(string Input, object Type)
        {
            foreach (var Arg in Input.Split('\n'))
            {
                var Args = Arg.Split('=');

                Type.GetType().GetRuntimeProperty(Args[0])?.SetValue(Type, Convert.ChangeType(Args[1].TrimEnd(), Type.GetType().GetRuntimeProperty(Args[0]).PropertyType));
            }

            return Type;
        }

        public T Deserialize<T>(string Input) => (T)Deserialize(Input, (T)(default));
    }
}