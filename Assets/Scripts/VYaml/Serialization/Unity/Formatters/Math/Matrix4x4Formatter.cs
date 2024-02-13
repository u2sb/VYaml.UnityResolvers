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
      var fs = new[]
      {
        new[] { value.m00, value.m10, value.m20, value.m30 },
        new[] { value.m01, value.m11, value.m21, value.m31 },
        new[] { value.m02, value.m12, value.m22, value.m32 },
        new[] { value.m03, value.m13, value.m23, value.m33 }
      };

      emitter.BeginSequence();
      foreach (var f in fs)
      {
        emitter.BeginSequence(SequenceStyle.Flow);
        foreach (var v in f) emitter.WriteFloat(v);
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