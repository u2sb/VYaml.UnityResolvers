#if VYAML_UNITY_RESOLVERS_ENABLE_MATHEMATICS
using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Matrix;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Int3x3Formatter : MatrixBaseFormatter<int3x3, int3>
  {
    public static readonly Int3x3Formatter Instance = new();

    public Int3x3Formatter() : base(3)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, int3x3 value, YamlSerializationContext context)
    {
      Buf[0] = value.c0;
      Buf[1] = value.c1;
      Buf[2] = value.c2;
      WriteArray(ref emitter, context);
    }

    public override int3x3 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser, context);
      return i switch
      {
        3 => new int3x3(Buf[0], Buf[1], Buf[2]),
        _ => default
      };
    }
  }
}
#endif