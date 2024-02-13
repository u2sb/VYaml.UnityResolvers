using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Vector3IntFormatter : IYamlFormatter<Vector3Int>
  {
    public static readonly Vector3IntFormatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Vector3Int value, YamlSerializationContext context)
    {
      var i = new[] { value.x, value.y, value.z };
      i.WriteIntArrayWithFlowStyle(ref emitter);
    }

    public Vector3Int Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var list = parser.ReadScalarAsIntArray(3);

      if (list.Length == 3) return new Vector3Int(list[0], list[1], list[2]);
      if (list.Length == 2) return new Vector3Int(list[0], list[1]);

      return default;
    }
  }
}