#if VYAML_UNITY_RESOLVERS_ENABLE_MATHEMATICS

using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class QuaternionFormatter : VectorFloatFormatter<quaternion>
  {
    public static readonly QuaternionFormatter Instance = new();

    public QuaternionFormatter() : base(4)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, quaternion value, YamlSerializationContext context)
    {
      Buf[0] = value.value.x;
      Buf[1] = value.value.y;
      Buf[2] = value.value.z;
      Buf[3] = value.value.w;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override quaternion Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        4 => new quaternion(Buf[0], Buf[1], Buf[2], Buf[3]),
        _ => default
      };
    }
  }
}

#endif