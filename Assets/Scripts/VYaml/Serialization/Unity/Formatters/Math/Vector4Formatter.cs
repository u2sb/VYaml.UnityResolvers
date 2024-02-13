using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Vector4Formatter : VectorFloatFormatter<Vector4>
  {
    public static readonly Vector4Formatter Instance = new();

    public Vector4Formatter() : base(4)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, Vector4 value, YamlSerializationContext context)
    {
      Buffer[0] = value.x;
      Buffer[1] = value.y;
      Buffer[2] = value.z;
      Buffer[3] = value.w;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override Vector4 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        4 => new Vector4(Buffer[0], Buffer[1], Buffer[2], Buffer[3]),
        3 => new Vector4(Buffer[0], Buffer[1], Buffer[2]),
        2 => new Vector4(Buffer[0], Buffer[1]),
        _ => default
      };
    }
  }
}