using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Vector2Formatter : VectorFloatFormatter<Vector2>
  {
    public static readonly Vector2Formatter Instance = new();

    public Vector2Formatter() : base(2)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, Vector2 value, YamlSerializationContext context)
    {
      Buffer[0] = value.x;
      Buffer[1] = value.y;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override Vector2 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        2 => new Vector2(Buffer[0], Buffer[1]),
        _ => default
      };
    }
  }
}