#if VYAML_UNITY_RESOLVERS_ENABLE_MATHEMATICS
using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Matrix;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Float4x4Formatter : MatrixBaseFormatter<float4x4, float4>
  {
    public static readonly Float4x4Formatter Instance = new();

    public Float4x4Formatter() : base(4)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, float4x4 value, YamlSerializationContext context)
    {
      Buf[0] = value.c0;
      Buf[1] = value.c1;
      Buf[2] = value.c2;
      Buf[3] = value.c3;
      WriteArray(ref emitter, context);
    }

    public override float4x4 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser, context);
      return i switch
      {
        4 => new float4x4(Buf[0], Buf[1], Buf[2], Buf[3]),
        _ => default
      };
    }
  }
}
#endif