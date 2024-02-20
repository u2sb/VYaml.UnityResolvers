#if ENABLE_MATHEMATICS
using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Matrix;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class UInt3x3Formatter : MatrixBaseFormatter<uint3x3, uint3>
  {
    public static readonly UInt3x3Formatter Instance = new();

    public UInt3x3Formatter() : base(3)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, uint3x3 value, YamlSerializationContext context)
    {
      Buf[0] = value.c0;
      Buf[1] = value.c1;
      Buf[2] = value.c2;
      WriteArray(ref emitter, context);
    }

    public override uint3x3 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser, context);
      return i switch
      {
        3 => new uint3x3(Buf[0], Buf[1], Buf[2]),
        _ => default
      };
    }
  }
}
#endif