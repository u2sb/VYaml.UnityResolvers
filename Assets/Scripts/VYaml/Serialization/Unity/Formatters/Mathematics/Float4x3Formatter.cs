#if VYAML_UNITY_RESOLVERS_ENABLE_MATHEMATICS
using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Matrix;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Float4x3Formatter : MatrixBaseFormatter<float4x3, float4>
  {
    public static readonly Float4x3Formatter Instance = new();

    public Float4x3Formatter() : base(3)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, float4x3 value, YamlSerializationContext context)
    {
      Buf[0] = value.c0;
      Buf[1] = value.c1;
      Buf[2] = value.c2;
      WriteArray(ref emitter, context);
    }

    public override float4x3 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser, context);
      return i switch
      {
        3 => new float4x3(Buf[0], Buf[1], Buf[2]),
        _ => default
      };
    }
  }
}
#endif