using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Color32Formatter : VectorByteFormatter<Color32>
  {
    public static readonly Color32Formatter Instance = new();

    public Color32Formatter() : base(4)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, Color32 value, YamlSerializationContext context)
    {
      Buf[0] = value.r;
      Buf[1] = value.g;
      Buf[2] = value.b;
      Buf[3] = value.a;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override Color32 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        4 => new Color32(Buf[0], Buf[1], Buf[2], Buf[3]),
        _ => default
      };
    }
  }
}