using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Vector2Formatter : IYamlFormatter<Vector2>
  {
    public static readonly Vector2Formatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Vector2 value, YamlSerializationContext context)
    {
      var f = new[] { value.x, value.y };
      f.WriteFloatArrayWithFlowStyle(ref emitter);
    }

    public Vector2 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var list = parser.ReadScalarAsFloatArray(2);

      if (list.Length == 2) return new Vector2(list[0], list[1]);

      return default;
    }
  }
}