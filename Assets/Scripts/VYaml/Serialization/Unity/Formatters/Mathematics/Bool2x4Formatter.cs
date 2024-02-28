#if VYAML_UNITY_RESOLVERS_ENABLE_MATHEMATICS
using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Matrix;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Bool2x4Formatter : MatrixBaseFormatter<bool2x4, bool2>
  {
    public static readonly Bool2x4Formatter Instance = new();

    public Bool2x4Formatter() : base(4)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, bool2x4 value, YamlSerializationContext context)
    {
      Buf[0] = value.c0;
      Buf[1] = value.c1;
      Buf[2] = value.c2;
      Buf[3] = value.c3;
      WriteArray(ref emitter, context);
    }

    public override bool2x4 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser, context);
      return i switch
      {
        4 => new bool2x4(Buf[0], Buf[1], Buf[2], Buf[3]),
        _ => default
      };
    }
  }
}
#endif