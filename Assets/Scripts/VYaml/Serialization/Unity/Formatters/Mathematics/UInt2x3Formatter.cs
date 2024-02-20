#if ENABLE_MATHEMATICS
using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Matrix;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class UInt2x3Formatter : MatrixBaseFormatter<uint2x3, uint2>
  {
    public static readonly UInt2x3Formatter Instance = new();

    public UInt2x3Formatter() : base(3)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, uint2x3 value, YamlSerializationContext context)
    {
      Buf[0] = value.c0;
      Buf[1] = value.c1;
      Buf[2] = value.c2;
      WriteArray(ref emitter, context);
    }

    public override uint2x3 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser, context);
      return i switch
      {
        3 => new uint2x3(Buf[0], Buf[1], Buf[2]),
        _ => default
      };
    }
  }
}
#endif