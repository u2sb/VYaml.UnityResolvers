#if ENABLE_MATHEMATICS

using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Int4Formatter : VectorIntFormatter<int4>
  {
    public static readonly Int4Formatter Instance = new();

    public Int4Formatter() : base(4)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, int4 value, YamlSerializationContext context)
    {
      Buf[0] = value.x;
      Buf[1] = value.y;
      Buf[2] = value.z;
      Buf[3] = value.w;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override int4 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        4 => new int4(Buf[0], Buf[1], Buf[2], Buf[3]),
        _ => default
      };
    }
  }
}

#endif