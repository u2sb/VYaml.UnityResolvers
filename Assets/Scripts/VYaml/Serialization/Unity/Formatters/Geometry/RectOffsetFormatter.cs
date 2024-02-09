using System.Collections.Generic;
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
      Utils.WriteIntArrayWithFlowStyle(ref emitter, i, context);
    }

    public RectOffset? Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var formatter = context.Resolver.GetFormatterWithVerify<List<int>>();

      var list = context.DeserializeWithAlias(formatter, ref parser);

      if (list.Count == 4) return new RectOffset(list[0], list[1], list[2], list[3]);

      return default;
    }
  }
}