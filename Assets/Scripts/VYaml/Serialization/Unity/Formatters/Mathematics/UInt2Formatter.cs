#if VYAML_UNITY_RESOLVERS_ENABLE_MATHEMATICS

using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class UInt2Formatter : VectorUIntFormatter<uint2>
  {
    public static readonly UInt2Formatter Instance = new();

    public UInt2Formatter() : base(2)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, uint2 value, YamlSerializationContext context)
    {
      Buf[0] = value.x;
      Buf[1] = value.y;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override uint2 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        2 => new uint2(Buf[0], Buf[1]),
        _ => default
      };
    }
  }
}
#endif