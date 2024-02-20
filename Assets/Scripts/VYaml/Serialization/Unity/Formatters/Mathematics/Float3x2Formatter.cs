#if ENABLE_MATHEMATICS
using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Matrix;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Float3x2Formatter : MatrixBaseFormatter<float3x2, float3>
  {
    public static readonly Float3x2Formatter Instance = new();

    public Float3x2Formatter() : base(2)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, float3x2 value, YamlSerializationContext context)
    {
      Buf[0] = value.c0;
      Buf[1] = value.c1;
      WriteArray(ref emitter, context);
    }

    public override float3x2 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser, context);
      return i switch
      {
        2 => new float3x2(Buf[0], Buf[1]),
        _ => default
      };
    }
  }
}
#endif