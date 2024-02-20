#if ENABLE_MATHEMATICS

using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Bool2Formatter : VectorBoolFormatter<bool2>
  {
    public static readonly Bool2Formatter Instance = new();

    public Bool2Formatter() : base(2)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, bool2 value, YamlSerializationContext context)
    {
      Buf[0] = value.x;
      Buf[1] = value.y;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override bool2 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        2 => new bool2(Buf[0], Buf[1]),
        _ => default
      };
    }
  }
}
#endif