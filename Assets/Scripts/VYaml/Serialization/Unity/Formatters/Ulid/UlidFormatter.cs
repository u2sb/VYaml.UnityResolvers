#if VYAML_UNITY_RESOLVERS_ENABLE_ULID
using System;
using System.Buffers.Text;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Ulid
{
  public class UlidFormatter : IYamlFormatter<System.Ulid>
  {
    public static readonly UlidFormatter Instance = new();

    private readonly byte[] _buffer = new byte[26];

    public void Serialize(ref Utf8YamlEmitter emitter, System.Ulid value, YamlSerializationContext context)
    {
      if (value.TryWriteStringify(_buffer))
      {
        emitter.WriteScalar(_buffer);
        return;
      }

      throw new YamlSerializerException($"Cannot serialize {value}");
    }

    public System.Ulid Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.TryGetScalarAsSpan(out var span))
      {
        parser.Read();
        if (System.Ulid.TryParse(span, out var ulid)) return ulid;
        if (Utf8Parser.TryParse(span, out Guid guid, out var bytesConsumed) &&
            bytesConsumed == span.Length) return new System.Ulid(guid);
      }

      throw new YamlSerializerException($"Cannot detect a scalar value of Ulid : {parser.CurrentEventType} {parser.GetScalarAsString()}");
    }
  }
}
#endif