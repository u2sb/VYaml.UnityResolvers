using System;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Base.Vector
{
  public abstract class VectorIntFormatter<T> : VectorBaseFormatter<T, int>
  {
    protected VectorIntFormatter(int l) : base(l)
    {
    }

    protected override void WriteArrayWithFlowStyle(ref Utf8YamlEmitter emitter)
    {
      emitter.BeginSequence(SequenceStyle.Flow);
      foreach (var b in Buf) emitter.WriteInt32(b);
      emitter.EndSequence();
    }

    protected override int ReadScalarAsArray(ref YamlParser parser)
    {
      var i = 0;
      parser.ReadWithVerify(ParseEventType.SequenceStart);
      while (!parser.End && parser.CurrentEventType != ParseEventType.SequenceEnd)
        if (i < Buf.Length)
        {
          Buf[i] = parser.ReadScalarAsInt32();
          i++;
        }
        else
        {
          parser.Read();
        }

      parser.ReadWithVerify(ParseEventType.SequenceEnd);
      return i;
    }
  }
}