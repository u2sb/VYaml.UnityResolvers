using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Geometry
{
  public class BoundsFormatter : IYamlFormatter<Bounds>
  {
    public static readonly BoundsFormatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Bounds value, YamlSerializationContext context)
    {
      var vector3Formatter = context.Resolver.GetFormatterWithVerify<Vector3>();
      emitter.BeginMapping();
      emitter.WriteString(nameof(Bounds.center));
      vector3Formatter.Serialize(ref emitter, value.center, context);

      emitter.WriteString(nameof(Bounds.size));
      vector3Formatter.Serialize(ref emitter, value.size, context);
      emitter.EndMapping();
    }

    public Bounds Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var bounds = new Bounds();
      var vector3Formatter = context.Resolver.GetFormatterWithVerify<Vector3>();

      parser.ReadWithVerify(ParseEventType.MappingStart);

      while (!parser.End && parser.CurrentEventType != ParseEventType.MappingEnd)
      {
        var key = parser.ReadScalarAsString();
        var value = context.DeserializeWithAlias(vector3Formatter, ref parser);

        if (key != null)
        {
          if (key.EqualsKey(nameof(Bounds.center)))
            bounds.center = value;
          else if (key.EqualsKey(nameof(Bounds.size)))
            bounds.size = value;
        }
      }

      parser.ReadWithVerify(ParseEventType.MappingEnd);

      return bounds;
    }
  }
}