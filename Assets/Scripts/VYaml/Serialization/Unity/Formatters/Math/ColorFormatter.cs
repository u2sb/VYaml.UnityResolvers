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
      Buf[0] = value.r;
      Buf[1] = value.g;
      Buf[2] = value.b;
      Buf[3] = value.a;
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
        4 => new Color(Buf[0], Buf[1], Buf[2], Buf[3]),
        3 => new Color(Buf[0], Buf[1], Buf[2]),
        _ => default
      };
    }
  }
}