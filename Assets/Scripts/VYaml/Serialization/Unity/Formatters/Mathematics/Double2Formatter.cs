#if ENABLE_MATHEMATICS

using Unity.Mathematics;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Mathematics
{
  public class Double2Formatter : VectorDoubleFormatter<double2>
  {
    public static readonly Double2Formatter Instance = new();

    public Double2Formatter() : base(2)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, double2 value, YamlSerializationContext context)
    {
      Buf[0] = value.x;
      Buf[1] = value.y;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override double2 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        2 => new double2(Buf[0], Buf[1]),
        _ => default
      };
    }
  }
}
#endif