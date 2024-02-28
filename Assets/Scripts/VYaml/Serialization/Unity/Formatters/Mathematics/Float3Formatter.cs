#if VYAML_UNITY_RESOLVERS_ENABLE_MATHEMATICS

using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Float3Formatter : VectorFloatFormatter<float3>
  {
    public static readonly Float3Formatter Instance = new();

    public Float3Formatter() : base(3)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, float3 value, YamlSerializationContext context)
    {
      Buf[0] = value.x;
      Buf[1] = value.y;
      Buf[2] = value.z;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override float3 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        3 => new float3(Buf[0], Buf[1], Buf[2]),
        _ => default
      };
    }
  }
}
#endif