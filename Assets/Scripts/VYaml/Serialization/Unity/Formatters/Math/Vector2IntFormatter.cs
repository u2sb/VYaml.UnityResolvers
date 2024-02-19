using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Vector2IntFormatter : VectorIntFormatter<Vector2Int>
  {
    public static readonly Vector2IntFormatter Instance = new();

    public Vector2IntFormatter() : base(2)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, Vector2Int value, YamlSerializationContext context)
    {
      Buf[0] = value.x;
      Buf[1] = value.y;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override Vector2Int Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);
      return i switch
      {
        2 => new Vector2Int(Buf[0], Buf[1]),
        _ => default
      };
    }
  }
}