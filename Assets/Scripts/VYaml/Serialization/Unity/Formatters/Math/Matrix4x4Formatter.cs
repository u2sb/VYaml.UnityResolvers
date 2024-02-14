using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Matrix4x4Formatter : IYamlFormatter<Matrix4x4>
  {
    public static readonly Matrix4x4Formatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Matrix4x4 value, YamlSerializationContext context)
    {
      emitter.BeginSequence();
      for (var i = 0; i < 4; i++)
      {
        emitter.BeginSequence(SequenceStyle.Flow);
        for (var j = 0; j < 4; j++) emitter.WriteFloat(value[i * 4 + j]);
        emitter.EndSequence();
      }

      emitter.EndSequence();
    }

    public Matrix4x4 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var value = new Matrix4x4();
      parser.ReadWithVerify(ParseEventType.SequenceStart);

      for (var i = 0; i < 4; i++)
      {
        parser.ReadWithVerify(ParseEventType.SequenceStart);
        for (var j = 0; j < 4; j++) value[i * 4 + j] = parser.ReadScalarAsFloat();
        parser.ReadWithVerify(ParseEventType.SequenceEnd);
      }

      parser.ReadWithVerify(ParseEventType.SequenceEnd);

      return value;
    }
  }
}