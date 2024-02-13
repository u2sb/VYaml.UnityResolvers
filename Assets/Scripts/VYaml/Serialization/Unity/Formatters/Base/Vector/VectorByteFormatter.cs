using System;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Base.Vector
{
  public class VectorByteFormatter<T> : VectorBaseFormatter<T, byte>
  {
    public VectorByteFormatter(int l) : base(l)
    {
    }


    public override void Serialize(ref Utf8YamlEmitter emitter, T value, YamlSerializationContext context)
    {
      throw new NotImplementedException();
    }

    public override T Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      throw new NotImplementedException();
    }

    protected override void WriteArrayWithFlowStyle(ref Utf8YamlEmitter emitter)
    {
      emitter.BeginSequence(SequenceStyle.Flow);
      foreach (var b in Buffer) emitter.WriteInt32(b);
      emitter.EndSequence();
    }

    protected override int ReadScalarAsArray(ref YamlParser parser)
    {
      var i = 0;
      parser.ReadWithVerify(ParseEventType.SequenceStart);
      while (!parser.End && parser.CurrentEventType != ParseEventType.SequenceEnd)
        if (i < Buffer.Length)
        {
          Buffer[i] = checked((byte)parser.ReadScalarAsUInt32());
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