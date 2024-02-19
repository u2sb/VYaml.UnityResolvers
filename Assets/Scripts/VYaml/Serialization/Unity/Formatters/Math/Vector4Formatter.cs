using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Vector4Formatter : VectorFloatFormatter<Vector4>
  {
    public static readonly Vector4Formatter Instance = new();

    public Vector4Formatter() : base(4)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, Vector4 value, YamlSerializationContext context)
    {
      Buf[0] = value.x;
      Buf[1] = value.y;
      Buf[2] = value.z;
      Buf[3] = value.w;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override Vector4 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        4 => new Vector4(Buf[0], Buf[1], Buf[2], Buf[3]),
        3 => new Vector4(Buf[0], Buf[1], Buf[2]),
        2 => new Vector4(Buf[0], Buf[1]),
        _ => default
      };
    }
  }
}