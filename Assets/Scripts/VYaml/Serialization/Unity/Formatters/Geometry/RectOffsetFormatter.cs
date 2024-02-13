using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Geometry
{
  public class RectOffsetFormatter : IYamlFormatter<RectOffset?>
  {
    public static readonly RectOffsetFormatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, RectOffset? value, YamlSerializationContext context)
    {
      if (value == null)
      {
        emitter.WriteNull();
        return;
      }

      var i = new[] { value.left, value.right, value.top, value.bottom };
      i.WriteIntArrayWithFlowStyle(ref emitter);
    }

    public RectOffset? Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var list = parser.ReadScalarAsIntArray(4);

      if (list.Length == 4) return new RectOffset(list[0], list[1], list[2], list[3]);

      return default;
    }
  }
}