#nullable enable
using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Geometry
{
  public class RectOffsetFormatter : VectorIntFormatter<RectOffset?>
  {
    public static readonly RectOffsetFormatter Instance = new();

    public RectOffsetFormatter() : base(4)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, RectOffset? value, YamlSerializationContext context)
    {
      if (value == null)
      {
        emitter.WriteNull();
        return;
      }

      Buffer[0] = value.left;
      Buffer[1] = value.right;
      Buffer[2] = value.top;
      Buffer[3] = value.bottom;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override RectOffset? Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        4 => new RectOffset(Buffer[0], Buffer[1], Buffer[2], Buffer[3]),
        _ => default
      };
    }
  }
}