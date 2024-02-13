using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Vector3Formatter : IYamlFormatter<Vector3>
  {
    public static readonly Vector3Formatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Vector3 value, YamlSerializationContext context)
    {
      var f = new[] { value.x, value.y, value.z };
      f.WriteFloatArrayWithFlowStyle(ref emitter);
    }

    public Vector3 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var list = parser.ReadScalarAsFloatArray(3);

      if (list.Length == 3) return new Vector3(list[0], list[1], list[2]);
      if (list.Length == 2) return new Vector3(list[0], list[1]);

      return default;
    }
  }
}