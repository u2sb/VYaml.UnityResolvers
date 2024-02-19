using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Base.Vector
{
  public abstract class VectorBaseFormatter<T, TU> : IYamlFormatter<T>
  {
    protected readonly TU[] Buf;

    protected VectorBaseFormatter(int l)
    {
      Buf = new TU[l];
    }

    public abstract void Serialize(ref Utf8YamlEmitter emitter, T value, YamlSerializationContext context);

    public abstract T Deserialize(ref YamlParser parser, YamlDeserializationContext context);

    protected abstract void WriteArrayWithFlowStyle(ref Utf8YamlEmitter emitter);

    protected abstract int ReadScalarAsArray(ref YamlParser parser);
  }
}