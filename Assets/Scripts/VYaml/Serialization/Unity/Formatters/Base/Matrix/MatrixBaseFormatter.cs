using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Base.Matrix
{
  public abstract class MatrixBaseFormatter<T, TU> : IYamlFormatter<T>
  {
    protected readonly TU[] Buf;

    protected MatrixBaseFormatter(int l)
    {
      Buf = new TU[l];
    }

    public abstract void Serialize(ref Utf8YamlEmitter emitter, T value, YamlSerializationContext context);

    public abstract T Deserialize(ref YamlParser parser, YamlDeserializationContext context);

    protected void WriteArray(ref Utf8YamlEmitter emitter, YamlSerializationContext context)
    {
      var formatter = context.Resolver.GetFormatterWithVerify<TU>();
      emitter.BeginSequence();
      foreach (var u in Buf) formatter.Serialize(ref emitter, u, context);
      emitter.EndSequence();
    }

    protected int ReadScalarAsArray(ref YamlParser parser, YamlDeserializationContext context)
    {
      var formatter = context.Resolver.GetFormatterWithVerify<TU>();
      var i = 0;
      parser.ReadWithVerify(ParseEventType.SequenceStart);
      while (!parser.End && parser.CurrentEventType != ParseEventType.SequenceEnd)
        if (i < Buf.Length)
        {
          Buf[i] = formatter.Deserialize(ref parser, context);
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