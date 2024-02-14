using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Geometry
{
  public class RectFormatter : IYamlFormatter<Rect>
  {
    public static readonly RectFormatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Rect value, YamlSerializationContext context)
    {
      var vector2Formatter = context.Resolver.GetFormatterWithVerify<Vector2>();

      emitter.BeginMapping();
      emitter.WriteString(nameof(Rect.position));
      vector2Formatter.Serialize(ref emitter, value.position, context);

      emitter.WriteString(nameof(Rect.size));
      vector2Formatter.Serialize(ref emitter, value.size, context);
      emitter.EndMapping();
    }

    public Rect Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var rect = new Rect();
      var vector2Formatter = context.Resolver.GetFormatterWithVerify<Vector2>();

      parser.ReadWithVerify(ParseEventType.MappingStart);

      while (!parser.End && parser.CurrentEventType != ParseEventType.MappingEnd)
      {
        var key = parser.ReadScalarAsString();
        var value = context.DeserializeWithAlias(vector2Formatter, ref parser);

        if (key != null)
        {
          if (key.EqualsKey(nameof(Rect.position)))
            rect.position = value;
          else if (key.EqualsKey(nameof(Rect.size)))
            rect.size = value;
        }
      }

      parser.ReadWithVerify(ParseEventType.MappingEnd);

      return rect;
    }
  }
}