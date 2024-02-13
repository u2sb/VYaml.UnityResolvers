using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class QuaternionFormatter : VectorFloatFormatter<Quaternion>
  {
    public static readonly QuaternionFormatter Instance = new();

    public QuaternionFormatter() : base(4)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, Quaternion value, YamlSerializationContext context)
    {
      Buffer[0] = value.x;
      Buffer[1] = value.y;
      Buffer[2] = value.z;
      Buffer[3] = value.w;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override Quaternion Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        4 => new Quaternion(Buffer[0], Buffer[1], Buffer[2], Buffer[3]),
        3 => Quaternion.Euler(Buffer[0], Buffer[1], Buffer[2]),
        _ => default
      };
    }
  }
}