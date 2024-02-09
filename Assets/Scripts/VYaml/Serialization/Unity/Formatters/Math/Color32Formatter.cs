using System.Collections.Generic;
using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Color32Formatter : IYamlFormatter<Color32>
  {
    public static readonly Color32Formatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Color32 value, YamlSerializationContext context)
    {
      var b = new[] { value.r, value.g, value.b, value.a };
      Utils.WriteByteArrayWithFlowStyle(ref emitter, b, context);
    }

    public Color32 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var formatter = context.Resolver.GetFormatterWithVerify<List<byte>>();

      var list = context.DeserializeWithAlias(formatter, ref parser);

      if (list.Count == 4) return new Color32(list[0], list[1], list[2], list[3]);

      return default;
    }
  }
}