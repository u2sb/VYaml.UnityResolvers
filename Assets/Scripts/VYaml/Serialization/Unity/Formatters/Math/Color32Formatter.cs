using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Vector;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Color32ByteFormatter : VectorByteFormatter<Color32>
  {
    public static readonly Color32ByteFormatter Instance = new();

    public Color32ByteFormatter() : base(4)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, Color32 value, YamlSerializationContext context)
    {
      Buffer[0] = value.r;
      Buffer[1] = value.g;
      Buffer[2] = value.b;
      Buffer[3] = value.a;
      WriteArrayWithFlowStyle(ref emitter);
    }

    public override Color32 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser);

      return i switch
      {
        4 => new Color32(Buffer[0], Buffer[1], Buffer[2], Buffer[3]),
        _ => default
      };
    }
  }
}