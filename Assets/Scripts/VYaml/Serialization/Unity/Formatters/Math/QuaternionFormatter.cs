using System.Collections.Generic;
using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class QuaternionFormatter : IYamlFormatter<Quaternion>
  {
    public static readonly QuaternionFormatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Quaternion value, YamlSerializationContext context)
    {
      var f = new[] { value.x, value.y, value.z, value.w };
      f.WriteFloatArrayWithFlowStyle(ref emitter);
    }

    public Quaternion Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var formatter = context.Resolver.GetFormatterWithVerify<List<float>>();

      var list = context.DeserializeWithAlias(formatter, ref parser);

      if (list.Count == 4) return new Quaternion(list[0], list[1], list[2], list[3]);
      if (list.Count == 3) return Quaternion.Euler(list[0], list[1], list[2]);

      return default;
    }
  }
}