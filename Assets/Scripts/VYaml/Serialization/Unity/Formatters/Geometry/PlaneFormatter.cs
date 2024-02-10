using System;
using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Geometry
{
  public class PlaneFormatter : IYamlFormatter<Plane>
  {
    public static readonly PlaneFormatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Plane value, YamlSerializationContext context)
    {
      emitter.BeginMapping();
      emitter.WriteString(nameof(Plane.normal));
      context.Serialize(ref emitter, value.normal);

      emitter.WriteString(nameof(Plane.distance));
      emitter.WriteFloat(value.distance);
      emitter.EndMapping();
    }

    public Plane Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var plane = new Plane();
      var vector3Formatter = context.Resolver.GetFormatterWithVerify<Vector3>();

      parser.ReadWithVerify(ParseEventType.MappingStart);

      while (!parser.End && parser.CurrentEventType != ParseEventType.MappingEnd)
      {
        var key = parser.ReadScalarAsString();

        if (key != null)
        {
          if (key.EqualsKey(nameof(Plane.normal)))
            plane.normal = context.DeserializeWithAlias(vector3Formatter, ref parser);
          else if (key.EqualsKey(nameof(Plane.distance)))
            plane.distance = parser.ReadScalarAsFloat();
        }
      }

      parser.ReadWithVerify(ParseEventType.MappingEnd);

      return plane;
    }
  }
}