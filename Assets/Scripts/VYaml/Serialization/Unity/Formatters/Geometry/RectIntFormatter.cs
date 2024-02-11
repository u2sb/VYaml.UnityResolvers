using System;
using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Geometry
{
  public class RectIntFormatter : IYamlFormatter<RectInt>
  {
    public static readonly RectIntFormatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, RectInt value, YamlSerializationContext context)
    {
      emitter.BeginMapping();
      emitter.WriteString(nameof(RectInt.position));
      context.Serialize(ref emitter, value.position);

      emitter.WriteString(nameof(RectInt.size));
      context.Serialize(ref emitter, value.size);
      emitter.EndMapping();
    }

    public RectInt Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var rectInt = new RectInt();
      var vector2IntFormatter = context.Resolver.GetFormatterWithVerify<Vector2Int>();

      parser.ReadWithVerify(ParseEventType.MappingStart);

      while (!parser.End && parser.CurrentEventType != ParseEventType.MappingEnd)
      {
        var key = parser.ReadScalarAsString();
        var value = context.DeserializeWithAlias(vector2IntFormatter, ref parser);

        if (key == null) continue;
        if (key.EqualsKey(nameof(rectInt.position)))
          rectInt.position = value;
        else if (key.EqualsKey(nameof(rectInt.size)))
          rectInt.size = value;
      }

      parser.ReadWithVerify(ParseEventType.MappingEnd);

      return rectInt;
    }
  }
}