using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Vector3IntFormatter : VectorIntFormatter<Vector3Int>
  {
    public static readonly Vector3IntFormatter Instance = new();

    public Vector3IntFormatter() : base(3)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, Vector3Int value, YamlSerializationContext context)
    {
      Buffer[0] = value.x;
      Buffer[1] = value.y;
      Buffer[2] = value.z;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override Vector3Int Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        3 => new Vector3Int(Buffer[0], Buffer[1], Buffer[2]),
        2 => new Vector3Int(Buffer[0], Buffer[1]),
        _ => default
      };
    }
  }
}