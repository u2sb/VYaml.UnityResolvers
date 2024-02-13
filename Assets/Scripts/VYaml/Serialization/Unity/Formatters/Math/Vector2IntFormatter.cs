using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Vector2IntFormatter : IYamlFormatter<Vector2Int>
  {
    public static readonly Vector2IntFormatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Vector2Int value, YamlSerializationContext context)
    {
      var i = new[] { value.x, value.y };
      i.WriteIntArrayWithFlowStyle(ref emitter);
    }

    public Vector2Int Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var list = parser.ReadScalarAsIntArray(2);
      if (list.Length == 2) return new Vector2Int(list[0], list[1]);

      return default;
    }
  }
}