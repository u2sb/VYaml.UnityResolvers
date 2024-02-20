#if ENABLE_MATHEMATICS
using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Matrix;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Double4x2Formatter : MatrixBaseFormatter<double4x2, double4>
  {
    public static readonly Double4x2Formatter Instance = new();

    public Double4x2Formatter() : base(2)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, double4x2 value, YamlSerializationContext context)
    {
      Buf[0] = value.c0;
      Buf[1] = value.c1;
      WriteArray(ref emitter, context);
    }

    public override double4x2 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser, context);
      return i switch
      {
        2 => new double4x2(Buf[0], Buf[1]),
        _ => default
      };
    }
  }
}
#endif