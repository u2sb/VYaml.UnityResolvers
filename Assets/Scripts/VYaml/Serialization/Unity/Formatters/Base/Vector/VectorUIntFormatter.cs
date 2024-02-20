using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Base.Vector
{
  public abstract class VectorUIntFormatter<T> : VectorBaseFormatter<T, uint>
  {
    protected VectorUIntFormatter(int l) : base(l)
    {
    }

    protected override void WriteArrayWithFlowStyle(ref Utf8YamlEmitter emitter)
    {
      emitter.BeginSequence(SequenceStyle.Flow);
      foreach (var b in Buf) emitter.WriteUInt32(b);
      emitter.EndSequence();
    }

    protected override int ReadScalarAsArray(ref YamlParser parser)
    {
      var i = 0;
      parser.ReadWithVerify(ParseEventType.SequenceStart);
      while (!parser.End && parser.CurrentEventType != ParseEventType.SequenceEnd)
        if (i < Buf.Length)
        {
          Buf[i] = parser.ReadScalarAsUInt32();
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