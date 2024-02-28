#if VYAML_UNITY_RESOLVERS_ENABLE_MATHEMATICS

using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Int3Formatter : VectorIntFormatter<int3>
  {
    public static readonly Int3Formatter Instance = new();

    public Int3Formatter() : base(3)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, int3 value, YamlSerializationContext context)
    {
      Buf[0] = value.x;
      Buf[1] = value.y;
      Buf[2] = value.z;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override int3 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        3 => new int3(Buf[0], Buf[1], Buf[2]),
        _ => default
      };
    }
  }
}
#endif