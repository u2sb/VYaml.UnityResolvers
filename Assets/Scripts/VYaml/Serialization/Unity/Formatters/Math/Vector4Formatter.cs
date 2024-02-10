using System.Collections.Generic;
using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Vector4Formatter : IYamlFormatter<Vector4>
  {
    public static readonly Vector4Formatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Vector4 value, YamlSerializationContext context)
    {
      var f = new[] { value.x, value.y, value.z, value.w };
      f.WriteFloatArrayWithFlowStyle(ref emitter);
    }

    public Vector4 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var formatter = context.Resolver.GetFormatterWithVerify<List<float>>();

      var list = context.DeserializeWithAlias(formatter, ref parser);

      if (list.Count == 4) return new Vector4(list[0], list[1], list[2], list[3]);
      if (list.Count == 3) return new Vector4(list[0], list[1], list[2]);
      if (list.Count == 2) return new Vector4(list[0], list[1]);

      return default;
    }
  }
}