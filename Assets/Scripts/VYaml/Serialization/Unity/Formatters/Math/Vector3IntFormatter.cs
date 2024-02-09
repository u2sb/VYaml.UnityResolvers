using System.Collections.Generic;
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
      var f = new[] { value.x, value.y, value.z };
      Utils.WriteIntArrayWithFlowStyle(ref emitter, f, context);
    }

    public Vector3Int Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var formatter = context.Resolver.GetFormatterWithVerify<List<int>>();

      var list = context.DeserializeWithAlias(formatter, ref parser);

      if (list.Count == 3) return new Vector3Int(list[0], list[1], list[2]);
      if (list.Count == 2) return new Vector3Int(list[0], list[1]);

      return default;
    }
  }
}