#if ENABLE_MATHEMATICS
using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Matrix;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Int4x3Formatter : MatrixBaseFormatter<int4x3, int4>
  {
    public static readonly Int4x3Formatter Instance = new();

    public Int4x3Formatter() : base(3)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, int4x3 value, YamlSerializationContext context)
    {
      Buf[0] = value.c0;
      Buf[1] = value.c1;
      Buf[2] = value.c2;
      WriteArray(ref emitter, context);
    }

    public override int4x3 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser, context);
      return i switch
      {
        3 => new int4x3(Buf[0], Buf[1], Buf[2]),
        _ => default
      };
    }
  }
}
#endif