using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class ColorFormatter : VectorFloatFormatter<Color>
  {
    public static readonly ColorFormatter Instance = new();

    public ColorFormatter() : base(4)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, Color value, YamlSerializationContext context)
    {
      Buffer[0] = value.r;
      Buffer[1] = value.g;
      Buffer[2] = value.b;
      Buffer[3] = value.a;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override Color Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        4 => new Color(Buffer[0], Buffer[1], Buffer[2], Buffer[3]),
        3 => new Color(Buffer[0], Buffer[1], Buffer[2]),
        _ => default
      };
    }
  }
}