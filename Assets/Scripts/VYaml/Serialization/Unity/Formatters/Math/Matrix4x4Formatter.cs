using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization.Unity.Formatters.Base.Matrix;

namespace VYaml.Serialization.Unity.Formatters.Math
{
  public class Matrix4x4Formatter : MatrixBaseFormatter<Matrix4x4, Vector4>
  {
    public static readonly Matrix4x4Formatter Instance = new();

    public Matrix4x4Formatter() : base(4)
    {
    }

    public override void Serialize(ref Utf8YamlEmitter emitter, Matrix4x4 value, YamlSerializationContext context)
    {
      Buf[0] = value.GetColumn(0);
      Buf[1] = value.GetColumn(1);
      Buf[2] = value.GetColumn(2);
      Buf[3] = value.GetColumn(3);
      WriteArray(ref emitter, context);
    }

    public override Matrix4x4 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var i = ReadScalarAsArray(ref parser, context);
      return i switch
      {
        4 => new Matrix4x4(Buf[0], Buf[1], Buf[2], Buf[3]),
        _ => default
      };
    }
  }
}