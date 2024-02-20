#if ENABLE_MATHEMATICS
using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Matrix;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Int2x2Formatter : MatrixBaseFormatter<int2x2, int2>
  {
    public static readonly Int2x2Formatter Instance = new();

    public Int2x2Formatter() : base(2)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, int2x2 value, YamlSerializationContext context)
    {
      Buf[0] = value.c0;
      Buf[1] = value.c1;
      WriteArray(ref emitter, context);
    }

    public override int2x2 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser, context);
      return i switch
      {
        2 => new int2x2(Buf[0], Buf[1]),
        _ => default
      };
    }
  }
}
#endif