#if VYAML_UNITY_RESOLVERS_ENABLE_MATHEMATICS

using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Half3Formatter : VectorHalfFormatter<half3>
  {
    public static readonly Half3Formatter Instance = new();

    public Half3Formatter() : base(3)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, half3 value, YamlSerializationContext context)
    {
      Buf[0] = value.x;
      Buf[1] = value.y;
      Buf[2] = value.z;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override half3 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        3 => new half3(Buf[0], Buf[1], Buf[2]),
        _ => default
      };
    }
  }
}
#endif