using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Vector3Formatter : VectorFloatFormatter<Vector3>
  {
    public static readonly Vector3Formatter Instance = new();

    public Vector3Formatter() : base(3)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, Vector3 value, YamlSerializationContext context)
    {
      Buf[0] = value.x;
      Buf[1] = value.y;
      Buf[2] = value.z;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override Vector3 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        3 => new Vector3(Buf[0], Buf[1], Buf[2]),
        2 => new Vector3(Buf[0], Buf[1]),
        _ => default
      };
    }
  }
}